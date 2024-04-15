using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AAFinanceTracker.Infrastructure.Repositories;

public abstract class GenericRepository<T>(DbContext context) : IRepository<T> where T : class
{
    public virtual async Task<EntityEntry<T>> Add(T entity, CancellationToken token)
    { 
        return await context.AddAsync(entity, token);
    }

    public virtual async Task<List<T>> All(CancellationToken token)
    {
        return await context.Set<T>()
            .AsQueryable()
            .ToListAsync(token);
    }

    public virtual void Delete(T entity)
    {
        context.Remove(entity);
    }

    public virtual async Task<List<T>> Find(Expression<Func<T, bool>> predicate, CancellationToken token)
    {
        return await context.Set<T>()
            .AsQueryable()
            .Where(predicate)
            .ToListAsync(token);
    }

    public virtual T Get(string id)
    {
        return context.Find<T>(id);
    }

    public async Task<int> SaveChangesAsync(CancellationToken token)
    {
        return await context.SaveChangesAsync(token);
    }

    public T Update(T entity)
    {
        return context.Update(entity).Entity;
    }
}