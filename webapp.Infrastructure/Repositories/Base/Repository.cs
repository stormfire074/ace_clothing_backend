using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using webapp.Application;
using webapp.Infrastrcture;

namespace webapp.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class, IIdentifiable
    {
        protected readonly DatabaseContext _dbcontext;

        public Repository(DatabaseContext _dbcontext)
        {
            this._dbcontext = _dbcontext;
        }

        public async Task<bool> AddAsync(T entity)
        {
            var flag = false;
            try
            {
                await _dbcontext.Set<T>().AddAsync(entity);
                await _dbcontext.SaveChangesAsync();
                flag = true;
            }
            catch (Exception ex)
            {
                throw;
            }


            return flag;
        }

        public async Task<DeleteReponse> DeleteAsync(Guid entity)
        {
            try
            {
                await _dbcontext.Set<T>().Where(x => x.Id == entity).ExecuteDeleteAsync();
                await _dbcontext.SaveChangesAsync();
                return new DeleteReponse(true);
            }
            catch (Exception ex)
            {
                return new DeleteReponse(false, ex);

            }
        }

        public async Task<ListResponse<T>> GetAllAsync(ListRequest request)
        {
            var query = _dbcontext.Set<T>().AsQueryable()
                .Skip(request.Skip).Take(request.Take);

            var totalCount = await query.CountAsync();
            var items = await query.ToListAsync();
            return new ListResponse<T>(request: request)
            {
                Entities = items,
                TotalCount = totalCount
            };

        }



        public async Task<T> GetByFieldNameAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbcontext?.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbcontext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateAsync(T entityToUpdate)
        {
            var flag = false;
            // Retrieves the entity using the ID to ensure it exists in the database
            try
            {
                var entity = await _dbcontext.Set<T>().FindAsync(entityToUpdate.Id);
                if (entity == null)
                {
                    throw new ArgumentException("Entity not found.");
                }

                // Maps the updated values to the tracked entity
                _dbcontext.Entry(entity).CurrentValues.SetValues(entityToUpdate);

                // Marks the entity as modified
                _dbcontext.Entry(entity).State = EntityState.Modified;

                // Saves changes asynchronously
                await _dbcontext.SaveChangesAsync();

                flag = true;
            }
            catch (Exception ex)
            {

                throw;
            }
            return flag;


        }
    }
}
