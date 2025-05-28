using B1SLayer;
using System.Text.Json.Nodes;
using webapp.Domain;

namespace webapp.Application
{
    public interface IServiceLayerService
    {
        Task<SaveResponse> AddAsync(SaveRequest<object> entity);
        Task<RetrieveResponse<object>> GetByIdAsync(JsonObject entity);
        Task<ListResponse<object>> GetAllAsync(JsonObject entity, int pageSize = 10, int pageNumber = 1);
        Task<SaveResponse> UpdateAsync(SaveRequest<object> entity);
        Task<DeleteReponse> DeleteAsync(JsonObject entity);
        Task<int> AddAttachmentsAsync(string filePath);
        Task<HttpResponseMessage[]> ExecuteBatchAsync(params SLBatchRequest[] batchRequests);
    }
}
