using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace AAFinanceTracker.Infrastructure.Repositories;

public abstract class GenericRepository<T>(DbContext context) : IRepository<T> where T : class
{
    public virtual T Add(T entity)
    {
        return context.Add(entity).Entity;
    }

    public virtual IQueryable<T> All()
    {
        return context.Set<T>().AsQueryable();
    }

    public virtual void Delete(T entity)
    {
        context.Remove(entity);
    }

    public virtual IQueryable<T> Find(Expression<Func<T, bool>> predicate)
    {
        return context.Set<T>()
            .AsQueryable()
            .Where(predicate);
    }

    public virtual T Get(string id)
    {
        return context.Find<T>(id);
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public T Update(T entity)
    {
        return context.Update(entity).Entity;
    }
}