using System.Linq.Expressions;
using AAExpenseTracker.Domain.Entities;

namespace AAFinanceTracker.Infrastructure.Repositories;

public interface IRepository<T>
{
    T Add(T entity);
    T Update(T entity);
    T Get(string id);
    T Delete(string id);
    IEnumerable<T> All();
    IEnumerable<T> Find(Expression<Func<T,bool>> predicate);
    void SaveChanges();
}

public class ExpensesRepository : IRepository<Expense>
{
    public Expense Add(Expense entity)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Expense> All()
    {
        throw new NotImplementedException();
    }

    public Expense Delete(string id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Expense> Find(Expression<Func<Expense, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Expense Get(string id)
    {
        throw new NotImplementedException();
    }

    public void SaveChanges()
    {
        throw new NotImplementedException();
    }

    public Expense Update(Expense entity)
    {
        throw new NotImplementedException();
    }
}