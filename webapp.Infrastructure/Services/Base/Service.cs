using Domain.Entities;
using System.Linq.Expressions;
using webapp.Application;

namespace webapp.Infrastrcture
{
    public class Service<T> : IService<T> where T : class, IIdentifiable
    {
        private readonly IRepository<T> repository;

        public Service(IRepository<T> repository)
        {
            this.repository = repository;
        }

        public virtual async Task<SaveResponse> AddAsync(T entity)
        {
            var res = await repository.AddAsync(entity);
            return new SaveResponse(isSaved: res);
        }

        public virtual async Task<DeleteReponse> DeleteAsync(Guid entity)
        {
            return await repository.DeleteAsync(entity);
        }

        public virtual async Task<ListResponse<T>> GetAllAsync(ListRequest request)
        {
            return await repository.GetAllAsync(request);
        }

        public virtual async Task<RetrieveResponse<T>> GetByFieldNameAsync(Expression<Func<T, bool>> predicate)
        {
            var response = await repository.GetByFieldNameAsync(predicate);
            return new RetrieveResponse<T>(response);
        }

        public virtual async Task<RetrieveResponse<T>> GetByIdAsync(Guid id)
        {
            var response = await repository.GetByIdAsync(id);
            return new RetrieveResponse<T>(response);
        }

        public virtual async Task<SaveResponse> UpdateAsync(T entity)
        {
            var response = await repository.UpdateAsync(entity);
            return new SaveResponse(isSaved: response);
        }

        
    }

}
