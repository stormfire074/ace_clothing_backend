using B1SLayer;
using System.Text.Json;
using System.Text.Json.Nodes;
using webapp.Application;
using webapp.Domain;

namespace webapp.Infrastructure
{
    public class ServiceLayerService : IServiceLayerService
    {
        private readonly SLConnection serviceLayer;

        public ServiceLayerService()
        {
            serviceLayer = SLConnectionManager.Connection();
        }

        public async Task<SaveResponse> AddAsync(SaveRequest<object> entity)
        {
            var _entity = JsonNode.Parse(entity.Entity.ToString())?.AsObject() ?? throw new InvalidOperationException("Invalid JSON format");

            if (!_entity.TryGetPropertyValue("Document", out var documentNode))
            {
                throw new InvalidOperationException("The 'Document' key is missing in the JSON data.");
            }

            string serviceLayerObjectName = documentNode!.ToString();
            _entity.Remove("Document");

            try
            {
                var obj = JsonSerializer.Deserialize<object>(_entity.ToJsonString());
                var createdEntity = await serviceLayer.Request(serviceLayerObjectName).PostAsync<object>(obj);
                var response = new SaveResponse(createdEntity);
                return response;
            }
            catch (Exception ex)
            {
                return new SaveResponse(exception: ex);
            }
        }

        public async Task<RetrieveResponse<object>> GetByIdAsync(JsonObject entity)
        {
            if (!entity.TryGetPropertyValue("Document", out var documentNode))
                throw new InvalidOperationException("The 'Document' key is missing in the JSON data.");

            if (!entity.TryGetPropertyValue("Key", out var keyNode))
                throw new InvalidOperationException("The 'Key' is missing in the JSON data.");

            string serviceLayerObjectName = documentNode!.ToString();
            string keyStr = keyNode!.ToString();
            object key = int.TryParse(keyStr, out int intKey) ? intKey : keyStr;

            entity.Remove("Document");
            entity.Remove("Key");

            try
            {
                var result = await serviceLayer.Request(serviceLayerObjectName, key).GetAsync<object>();
                var response = new RetrieveResponse<object>(result);
                return response;
            }
            catch (Exception ex)
            {
                return new RetrieveResponse<object>(null, ex);
            }
        }

        public async Task<ListResponse<object>> GetAllAsync(JsonObject entity, int pageSize = 10, int pageNumber = 1)
        {
            if (!entity.TryGetPropertyValue("Document", out var documentNode))
                throw new InvalidOperationException("The 'Document' key is missing in the JSON data.");

            string serviceLayerObjectName = documentNode!.ToString();
            entity.Remove("Document");

            try
            {
                int skip = (pageNumber - 1) * pageSize;

                var count = await serviceLayer.Request(serviceLayerObjectName).GetCountAsync();

                var entities = await serviceLayer.Request(serviceLayerObjectName)
                    .WithPageSize(pageSize)
                    .Skip(skip)
                    .GetAsync<List<object>>();

                return new ListResponse<object>(entities, new ListRequest(pageNumber, pageSize), count);
            }
            catch (Exception ex)
            {
                return new ListResponse<object>(exception: ex);
            }
        }

        public async Task<SaveResponse> UpdateAsync(SaveRequest<object> request)
        {
            var entity = JsonNode.Parse(request.Entity.ToString())?.AsObject();
            if (!entity.TryGetPropertyValue("Document", out var documentNode))
                throw new InvalidOperationException("The 'Document' key is missing in the JSON data.");

            if (!entity.TryGetPropertyValue("Key", out var keyNode))
                throw new InvalidOperationException("The 'Key' is missing in the JSON data.");

            string serviceLayerObjectName = documentNode!.ToString();
            string key = keyNode!.ToString();

            entity.Remove("Document");
            entity.Remove("Key");

            try
            {
                var payload = JsonSerializer.Serialize(entity);
                await serviceLayer.Request(serviceLayerObjectName, key).PatchAsync(payload);

                var updatedEntity = await GetByIdAsync(new JsonObject
                {
                    ["Document"] = serviceLayerObjectName,
                    ["Key"] = key
                });
                var response = new SaveResponse(updatedEntity);
                return response;

            }
            catch (Exception ex)
            {
                return new SaveResponse(exception: ex);
            }
        }

        public async Task<DeleteReponse> DeleteAsync(JsonObject entity)
        {
            if (!entity.TryGetPropertyValue("Document", out var documentNode))
                throw new InvalidOperationException("The 'Document' key is missing in the JSON data.");

            if (!entity.TryGetPropertyValue("Key", out var keyNode))
                throw new InvalidOperationException("The 'Key' is missing in the JSON data.");

            string serviceLayerObjectName = documentNode!.ToString();
            string key = keyNode!.ToString();

            entity.Remove("Document");
            entity.Remove("Key");

            try
            {
                await serviceLayer.Request(serviceLayerObjectName, key).DeleteAsync();
                return new DeleteReponse(true);
            }
            catch (Exception ex)
            {
                return new DeleteReponse(false, ex);
            }
        }

        public async Task<int> AddAttachmentsAsync(string filePath)
        {
            try
            {
                var attachmentEntry = await serviceLayer.PostAttachmentAsync(filePath);
                return attachmentEntry.AbsoluteEntry;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add attachment: " + ex.Message, ex);
            }
        }

        public async Task<HttpResponseMessage[]> ExecuteBatchAsync(params SLBatchRequest[] batchRequests)
        {
            try
            {
                var batchResult = await serviceLayer.PostBatchAsync(batchRequests);
                return batchResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to execute batch operations: " + ex.Message, ex);
            }
        }
    }
}
