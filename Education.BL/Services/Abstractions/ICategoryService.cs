using Education.BL.DTOs;
using Education.Core.Models;

namespace Education.BL.Services.Abstractions;

public interface ICategoryService
{
    int TotalCount { get; }
    Task<int> TotalSearchCountAsync(string search);
    Task<ICollection<CategoryListItemDTO>> GetAllListItemsAsync(string? search = null, int page = 0, int count = 5);
    Task<ICollection<CategoryViewItemDTO>> GetAllViewItemsAsync(int count = 3);
    Task<Category> GetByIdAsync(int id);
    Task<CategoryUpdateDTO> GetByIdForUpdateAsync(int id);
    Task CreateAsync(CategoryCreateDTO dto, string username);
    Task UpdateAsync(CategoryUpdateDTO dto, string username);
    Task DeleteAsync(int id);
    Task<int> SaveChangesAsync();
}
