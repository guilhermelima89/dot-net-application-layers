using System.Linq.Expressions;
using Core.Models;

namespace Core.Interfaces;

public interface IRepository<T> where T : Entity
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate);

    void Add(T entity);
    void AddRange(List<T> entities);
    void Update(T entity);
    void UpdateRange(List<T> entities);
    void Remove(int id);
    void RemoveRange(IEnumerable<T> entities);
}
