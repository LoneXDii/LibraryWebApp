using LibraryServer.Domain.Entities.Abstractions;
using System.Linq.Expressions;

namespace LibraryServer.DataAccess.Repositories.Interfaces;

public interface IRepository<T> where T : Entity
{
    Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includedProperties);
    Task<IEnumerable<T>> ListAllAsync();
    Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter,
                                   params Expression<Func<T, object>>[] includedProperties);
    Task<(IEnumerable<T>, int)> ListWithPaginationAsync(int pageNo, int pageSize,
                                                        Expression<Func<T, bool>>? filter = null,
                                                        params Expression<Func<T, object>>[] includedProperties);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter);
}
