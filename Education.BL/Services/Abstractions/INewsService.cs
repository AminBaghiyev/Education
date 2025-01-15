using Education.BL.DTOs;
using Education.Core.Models;

namespace Education.BL.Services.Abstractions;

public interface INewsService
{
    int TotalCount { get; }
    Task<int> TotalSearchCountAsync(string search);
    Task<ICollection<NewsListItemDTO>> GetAllListItemsAsync(string? search = null, int page = 0, int count = 5);
    Task<ICollection<NewsViewItemDTO>> GetAllViewItemsAsync(int count = 3);
    Task<News> GetByIdAsync(int id);
    Task<NewsUpdateDTO> GetByIdForUpdateAsync(int id);
    Task CreateAsync(NewsCreateDTO dto, string username);
    Task UpdateAsync(NewsUpdateDTO dto, string username);
    Task DeleteAsync(int id);
    Task<int> SaveChangesAsync();
}
