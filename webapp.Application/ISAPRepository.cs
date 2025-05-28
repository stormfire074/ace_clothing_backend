using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webapp.Domain;

namespace webapp.Application
{
    public interface ISAPRepository<T> where T : class
    {
        Task<IList<T>> GetAllAsync();
        Task<T> GetByIdAsync(dynamic id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<DeleteReponse> DeleteAsync(T entity);
        Task<int> AddAttachments(IDictionary<string, Stream> files);
    }
}
