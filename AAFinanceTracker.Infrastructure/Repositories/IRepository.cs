using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace AAFinanceTracker.Infrastructure.Repositories;

public interface IRepository<T> where T : class
{
    Task<EntityEntry<T>> Add(T entity, CancellationToken token);
    Task<List<T>> All(CancellationToken token);
    T Update(T entity);
    Task<T> Get(string id, CancellationToken token);
    void Delete(T entity);
    Task<List<T>> Find(Expression<Func<T, bool>> predicate, CancellationToken token);
    Task<int> SaveChangesAsync(CancellationToken token);
}