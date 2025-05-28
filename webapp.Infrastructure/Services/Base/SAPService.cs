using webapp.Application;
using webapp.Domain;

namespace webapp.Infrastrcture
{
    public class SAPService<T> : ISAPService<T> where T : class
    {

        ISAPRepository<T> isapRepository;
        public SAPService(ISAPRepository<T> isapRepository)
        {
            this.isapRepository = isapRepository;
        }
        
        public async Task<T> AddAsync(T entity)
        {
            return await isapRepository.AddAsync(entity);
        }

        public async Task<DeleteReponse> DeleteAsync(T entity)
        {
           return await isapRepository.DeleteAsync(entity);
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await isapRepository.GetAllAsync();
        }

        public async Task<T> GetByIdAsync(dynamic id)
        {
            return await isapRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            await isapRepository.UpdateAsync(entity);
        }
    }
}
