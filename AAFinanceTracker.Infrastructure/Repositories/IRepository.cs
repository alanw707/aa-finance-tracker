using System.Linq.Expressions;

namespace AAFinanceTracker.Infrastructure.Repositories;

public interface IRepository<T>
{
    T Add(T entity);
    T Update(T entity);
    T Get(string id);
    void Delete(T entity);
    IQueryable<T> All();
    IQueryable<T> Find(Expression<Func<T,bool>> predicate);
    void SaveChanges();
}