using Education.Core.Models.Base;
using Education.DL.Contexts;
using Education.DL.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Education.DL.Repository.Implementations;

public class Repository<T> : IRepository<T> where T : BaseEntity, new()
{
    readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public async Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, int page = 0, int count = 5, bool OrderAsc = true, params string[] includes)
    {
        IQueryable<T> query = Table.AsQueryable().AsNoTracking();

        if (includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        if (predicate is not null) query = query.Where(predicate);

        query = OrderAsc ? query.OrderBy(x => x.Id) : query.OrderByDescending(x => x.Id);

        if (count > 0) query = query.Skip(page * count).Take(count);

        return await query.ToListAsync();
    }

    public Task<T?> GetOneAsync(Expression<Func<T, bool>> predicate, bool isTracking = false, params string[] includes)
    {
        IQueryable<T> query = Table.AsQueryable();

        if (!isTracking) query = query.AsNoTracking();

        if (includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return query.SingleOrDefaultAsync(predicate);
    }

    public async Task CreateAsync(T entity)
    {
        await Table.AddAsync(entity);
    }

    public void Update(T entity)
    {
        Table.Update(entity);
    }

    public void Delete(T entity)
    {
        Table.Remove(entity);
    }

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
}
