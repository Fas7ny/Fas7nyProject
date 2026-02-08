using System.Linq.Expressions;

namespace Fas7ny.Domain.RepoInterfaces
{
    public interface IGenericRepository<T> where T : class
    {
        // Get Operations
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Query();

        Task<(IEnumerable<T> Items, int TotalCount)> GetPagedWithIncludesAsync(
    int page,
    int pageSize,
    params Expression<Func<T, object>>[] includes
);

        // FIX: Add method to find single item
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);


        // FIX: Add method to find multiple items
        Task<IEnumerable<T>> FindManyAsync(Expression<Func<T, bool>> predicate);

        // Get with includes
        Task<IEnumerable<T>> GetAllWithIncludesAsync(params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdWithIncludesAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> FindWithIncludesAsync(
    Expression<Func<T, bool>> predicate,
    params Expression<Func<T, object>>[] includes
);

        // Add Operations
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);

        // Update Operations
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(IEnumerable<T> entities);

        // Delete Operations
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entities);
        Task DeleteByIdAsync(int id);

        // Additional Operations
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        // Pagination
        Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(
            int pageNumber,
            int pageSize,
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task<IEnumerable<T>> FindAllAsync(
            Expression<Func<T, bool>> predicate
        );
    }
}
