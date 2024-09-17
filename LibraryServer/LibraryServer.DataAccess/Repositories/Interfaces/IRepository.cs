using LibraryServer.DataAccess.Entities.Abstractions;
using System.Linq.Expressions;

namespace LibraryServer.DataAccess.Repositories.Interfaces;

public interface IRepository<T> where T : Entity
{
    Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includedProperties);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> filter,
                                     params Expression<Func<T, object>>[] includedProperties);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter);
}
