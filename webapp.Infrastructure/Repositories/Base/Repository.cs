using Domain.Entities;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using webapp.Application;
using webapp.Infrastrcture;

namespace webapp.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class, IIdentifiable
    {
        protected readonly DatabaseContext _dbcontext;

        public Repository(DatabaseContext dbcontext)
        {
            this._dbcontext = dbcontext;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbcontext.Set<T>().AddAsync(entity);
            await _dbcontext.SaveChangesAsync();
            return entity;
        }

        public async Task<DeleteReponse> DeleteAsync(T entity)
        {
            try
            {
                _dbcontext.Set<T>().Remove(entity);
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
            return new ListResponse<T>
            {
                Entities = items,
                TotalCount = totalCount
            };

        }

        

        public async Task<T> GetByFieldNameAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbcontext?.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbcontext.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entityToUpdate)
        {
            // Retrieves the entity using the ID to ensure it exists in the database
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
        }
    }
}
