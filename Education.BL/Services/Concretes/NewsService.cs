using AutoMapper;
using Education.BL.DTOs;
using Education.BL.Exceptions;
using Education.BL.Services.Abstractions;
using Education.BL.Utilities;
using Education.Core.Models;
using Education.DL.Repository.Abstractions;

namespace Education.BL.Services.Concretes;

public class NewsService : INewsService
{
    readonly IRepository<News> _repository;
    readonly IRepository<Category> _categoryRepository;
    readonly IMapper _mapper;

    public NewsService(IRepository<News> repository, IMapper mapper, IRepository<Category> categoryRepository)
    {
        _repository = repository;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    public int TotalCount => _repository.Table.Count();

    public async Task<int> TotalSearchCountAsync(string search)
    {
        ICollection<News> news = await _repository.GetAllAsync(e => e.Title.Contains(search), count: 0);

        return news.Count;
    }

    public async Task<ICollection<NewsListItemDTO>> GetAllListItemsAsync(string? search = null, int page = 0, int count = 5)
    {
        if (search is null) return _mapper.Map<ICollection<NewsListItemDTO>>(await _repository.GetAllAsync(page: page, count: count));
        
        return _mapper.Map<ICollection<NewsListItemDTO>>(await _repository.GetAllAsync(e => e.Title.Contains(search), page, count));
    }

    public async Task<ICollection<NewsViewItemDTO>> GetAllViewItemsAsync(int count = 3) => _mapper.Map<ICollection<NewsViewItemDTO>>(await _repository.GetAllAsync(count: count, OrderAsc: false));

    public async Task<News> GetByIdAsync(int id) => await _repository.GetOneAsync(e => e.Id == id, includes: "Category") ?? throw new Exception();

    public async Task<NewsUpdateDTO> GetByIdForUpdateAsync(int id) => _mapper.Map<NewsUpdateDTO>(await GetByIdAsync(id));

    public async Task CreateAsync(NewsCreateDTO dto, string username)
    {
        News news = _mapper.Map<News>(dto);

        if (await _categoryRepository.GetOneAsync(e => e.Id == news.CategoryId) is null) throw new CategoryNotFoundException();

        news.ThumbnailPath = await dto.Thumbnail.SaveAsync("news");
        news.CreatedBy = username;
        news.CreatedAt = DateTime.UtcNow.AddHours(4);

        await _repository.CreateAsync(news);
    }

    public async Task DeleteAsync(int id)
    {
        News news = await GetByIdAsync(id);

        _repository.Delete(news);

        File.Delete(Path.Combine(Path.GetFullPath("wwwroot"), "uploads", "news", news.ThumbnailPath));
    }

    public async Task UpdateAsync(NewsUpdateDTO dto, string username)
    {
        News oldNews = await GetByIdAsync(dto.Id);
        News news = _mapper.Map<News>(dto);

        if (await _categoryRepository.GetOneAsync(e => e.Id == news.CategoryId) is null) throw new CategoryNotFoundException();

        news.ThumbnailPath = dto.Thumbnail is not null ? await dto.Thumbnail.SaveAsync("news") : oldNews.ThumbnailPath;
        news.CreatedBy = oldNews.CreatedBy;
        news.CreatedAt = oldNews.CreatedAt;
        news.UpdatedBy = username;
        news.UpdatedAt = DateTime.UtcNow.AddHours(4);

        _repository.Update(news);

        if (dto.Thumbnail is not null) File.Delete(Path.Combine(Path.GetFullPath("wwwroot"), "uploads", "news", oldNews.ThumbnailPath));
    }

    public async Task<int> SaveChangesAsync() => await _repository.SaveChangesAsync();
}
