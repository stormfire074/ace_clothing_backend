using Domain.Entities;
using System.Linq.Expressions;

namespace webapp.Application
{
    public interface IRepository<T> where T : class, IIdentifiable
    {
        Task<ListResponse<T>> GetAllAsync(ListRequest request);
        Task<T> GetByIdAsync(int id);
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<DeleteReponse> DeleteAsync(T entity);
        Task<T> GetByFieldNameAsync(Expression<Func<T, bool>> predicate);
    }
}
