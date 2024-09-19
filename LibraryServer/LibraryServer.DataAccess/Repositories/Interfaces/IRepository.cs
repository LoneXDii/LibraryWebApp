using LibraryServer.Domain.Entities.Abstractions;
using LibraryServer.Domain.Common.Models;
using System.Linq.Expressions;

namespace LibraryServer.DataAccess.Repositories.Interfaces;

public interface IRepository<T> where T : IEntity
{
    Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includedProperties);
    Task<IEnumerable<T>> ListAllAsync();
    Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter,
                                   params Expression<Func<T, object>>[] includedProperties);
    Task<PaginatedListModel<T>> ListWithPaginationAsync(int pageNo, int pageSize,
                                                        Expression<Func<T, bool>>? filter = null,
                                                        params Expression<Func<T, object>>[] includedProperties);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> filter);
}
