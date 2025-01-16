using Klinik.Core.Models.Base;
using Klinik.DL.Contexts;
using Klinik.DL.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Klinik.DL.Repository.Implementations;

public class Repository<T> : IRepository<T> where T : BaseEntity, new()
{
    readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public async Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null, int page = 0, int count = 5, bool orderAsc = true, params string[] includes)
    {
        IQueryable<T> query = Table.AsQueryable().AsNoTracking();

        if (includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        if (expression is not null) query = query.Where(expression);

        if (count > 0) query = query.Skip(page * count).Take(count);

        query = orderAsc ? query.OrderBy(x => x.Id) : query.OrderByDescending(x => x.Id);

        return await query.ToListAsync();
    }

    public Task<T?> GetOneAsync(Expression<Func<T, bool>> expression, bool isTracking = false, params string[] includes)
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

        return query.SingleOrDefaultAsync(expression);
    }

    public async Task CreateAsync(T entity)
    {
        await Table.AddAsync(entity);
    }

    public void Delete(T entity)
    {
        Table.Remove(entity);
    }

    public void Update(T entity)
    {
        Table.Update(entity);
    }

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
}
