using System.Linq.Expressions;
using Core.Interfaces;
using Core.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public abstract class Repository<T> : IRepository<T> where T : Entity, new()
{
    protected readonly ApplicationDbContext Context;
    protected readonly DbSet<T> DbSet;

    protected Repository(ApplicationDbContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await DbSet
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await DbSet
            .Select(x => new T
            {
                Id = x.Id
            })
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate)
    {
        return await DbSet
            .Where(predicate)
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync();
    }

    public void Add(T entity)
    {
        DbSet.Add(entity);
    }

    public void AddRange(List<T> entities)
    {
        DbSet.AddRange(entities);
    }

    public void Update(T entity)
    {
        DbSet.Update(entity);
    }

    public void UpdateRange(List<T> entities)
    {
        DbSet.UpdateRange(entities);
    }

    public void Remove(int id)
    {
        DbSet.Remove(new T { Id = id });
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        DbSet.RemoveRange(entities);
    }
}
