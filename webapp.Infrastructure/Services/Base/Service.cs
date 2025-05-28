using Azure;
using System.Linq.Expressions;
using webapp.Application;
using webapp.Domain;

namespace webapp.Infrastrcture
{
    public class Service<T> : IService<T> where T : class, IIdentifiable
    {
        private readonly IRepository<T> repository;

        public Service(IRepository<T> repository)
        {
            this.repository = repository;
        }

        public virtual Task<T> AddAsync(T entity)
        {
            var res = repository.AddAsync(entity);
            return repository.GetByIdAsync(res.Id);
        }

        public virtual Task<DeleteReponse> DeleteAsync(T entity)
        {
            return repository.DeleteAsync(entity);
        }

        public virtual Task<ListResponse<T>> GetAllAsync(ListRequest request)
        {
            return repository.GetAllAsync(request);
        }

        public async Task<T> GetByFieldNameAsync(Expression<Func<T, bool>> predicate)
        {
            return await repository.GetByFieldNameAsync(predicate);
        }

        public virtual Task<T> GetByIdAsync(int id)
        {
            return repository.GetByIdAsync(id);
        }

        public virtual Task UpdateAsync(T entity)
        {
            return repository.UpdateAsync(entity);
        }


    }

}
