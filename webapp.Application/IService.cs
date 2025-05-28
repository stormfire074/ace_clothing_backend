using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using webapp.Domain;

namespace webapp.Application
{
    public interface IService<T> where T : class
    {
        Task<ListResponse<T>> GetAllAsync(ListRequest<T> request);
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<DeleteReponse> DeleteAsync(T entity);
        Task<T> GetByFieldNameAsync(Expression<Func<T, bool>> predicate);

    }
}
