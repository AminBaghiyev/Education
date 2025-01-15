using Education.Core.Models.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Education.DL.Repository.Abstractions;

public interface IRepository<T> where T : BaseEntity, new()
{
    DbSet<T> Table { get; }
    Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, int page = 0, int count = 5, bool OrderAsc = true, params string[] includes);
    Task<T?> GetOneAsync(Expression<Func<T, bool>> predicate, bool isTracking = false, params string[] includes);
    Task CreateAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<int> SaveChangesAsync();
}
