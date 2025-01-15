using AutoMapper;
using Education.BL.DTOs;
using Education.BL.Services.Abstractions;
using Education.Core.Models;
using Education.DL.Repository.Abstractions;

namespace Education.BL.Services.Concretes;

public class CategoryService : ICategoryService
{
    readonly IRepository<Category> _repository;
    readonly IMapper _mapper;

    public CategoryService(IRepository<Category> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public int TotalCount => _repository.Table.Count();

    public async Task<int> TotalSearchCountAsync(string search)
    {
        ICollection<Category> categories = await _repository.GetAllAsync(e => e.Title.Contains(search), count: 0);

        return categories.Count;
    }

    public async Task<ICollection<CategoryListItemDTO>> GetAllListItemsAsync(string? search = null, int page = 0, int count = 5)
    {
        if (search is null) return _mapper.Map<ICollection<CategoryListItemDTO>>(await _repository.GetAllAsync(page: page, count: count));

        return _mapper.Map<ICollection<CategoryListItemDTO>>(await _repository.GetAllAsync(e => e.Title.Contains(search), page, count));
    }

    public async Task<ICollection<CategoryViewItemDTO>> GetAllViewItemsAsync(int count = 3) => _mapper.Map<ICollection<CategoryViewItemDTO>>(await _repository.GetAllAsync(count: count));

    public async Task<Category> GetByIdAsync(int id) => await _repository.GetOneAsync(e => e.Id == id) ?? throw new Exception();

    public async Task<CategoryUpdateDTO> GetByIdForUpdateAsync(int id) => _mapper.Map<CategoryUpdateDTO>(await GetByIdAsync(id));

    public async Task CreateAsync(CategoryCreateDTO dto, string username)
    {
        Category category = _mapper.Map<Category>(dto);
        category.CreatedBy = username;
        category.CreatedAt = DateTime.UtcNow.AddHours(4);

        await _repository.CreateAsync(category);
    }

    public async Task DeleteAsync(int id)
    {
        Category category = await _repository.GetOneAsync(e => e.Id == id, includes: "News") ?? throw new Exception();

        if (category.News.Count != 0) throw new Exception();

        _repository.Delete(category);
    }

    public async Task UpdateAsync(CategoryUpdateDTO dto, string username)
    {
        Category oldCategory = await GetByIdAsync(dto.Id);
        Category category = _mapper.Map<Category>(dto);

        category.CreatedBy = oldCategory.CreatedBy;
        category.CreatedAt = oldCategory.CreatedAt;
        category.UpdatedBy = username;
        category.UpdatedAt = DateTime.UtcNow.AddHours(4);

        _repository.Update(category);
    }

    public async Task<int> SaveChangesAsync() => await _repository.SaveChangesAsync();
}
