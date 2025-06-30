using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace webapp.Application
{
    public interface IService<T> where T : class
    {
        Task<ListResponse<T>> GetAllAsync(ListRequest request);
        Task<RetrieveResponse<T>> GetByIdAsync(Guid id);
        Task<SaveResponse> AddAsync(T entity);
        Task<SaveResponse> UpdateAsync(T entity);
        Task<DeleteReponse> DeleteAsync(Guid id);
        Task<RetrieveResponse<T>> GetByFieldNameAsync(Expression<Func<T, bool>> predicate);

    }
}
