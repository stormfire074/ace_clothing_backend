using B1SLayer;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using webapp.Application;
using webapp.Domain;
using webapp.Infrastructure;
using webapp.SharedServies;

namespace webapp.Infrastrcture
{
    public class SAPRepository<T> : ISAPRepository<T> where T : class
    {

        public SAPRepository()
        {
        }
        
        public async Task<T> AddAsync(T entity)
        {
            var serviceLayerObjectName = typeof(T).GetCustomAttribute<ServiceLayerObjectNameAttribute>()?.Name;
            if (string.IsNullOrEmpty(serviceLayerObjectName))
            {
                throw new InvalidOperationException("The ServiceLayerObjectName attribute is not defined on the class " + typeof(T).Name);
            }

            try
            {
                var response = await SLConnectionManager.Connection().Request(serviceLayerObjectName).PostAsync<T>(entity);
                return response;  
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create entity: " + ex.Message, ex);
            }
        }

        public async Task<int> AddAttachments(IDictionary<string, Stream> files)
        {
            var attachment = await SLConnectionManager.Connection().PostAttachmentsAsync(files);
            return attachment.AbsoluteEntry;
        }

        public async Task<DeleteReponse> DeleteAsync(T entity)
        {
            var serviceLayerObjectName = typeof(T).GetCustomAttribute<ServiceLayerObjectNameAttribute>()?.Name;
            if (string.IsNullOrEmpty(serviceLayerObjectName))
            {
                throw new InvalidOperationException("The ServiceLayerObjectName attribute is not defined on the class " + typeof(T).Name);
            }
            var PKVal = typeof(T).GetProperties().FirstOrDefault(prop => prop.GetCustomAttributes(typeof(SAPPrimaryKeyAttribute), false).Length > 0)?.GetValue(entity);

            try
            {
                await SLConnectionManager.Connection().Request(serviceLayerObjectName, PKVal).DeleteAsync();
                return new DeleteReponse(true);
            }
            catch (Exception ex)
            {
                return new DeleteReponse(false, ex);
                
            }
            

        }

        public async Task<IList<T>> GetAllAsync()
        {
            var serviceLayerObjectName = typeof(T).GetCustomAttribute<ServiceLayerObjectNameAttribute>()?.Name;
            if (string.IsNullOrEmpty(serviceLayerObjectName))
            {
                throw new InvalidOperationException("The ServiceLayerObjectName attribute is not defined on the class " + typeof(T).Name);
            }
            var selectProperties = typeof(T).GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(SelectColumnAttribute)))
                .Select(prop => prop.Name)
                .ToList();

            var selectClause = string.Join(",", selectProperties);

            var orderByProperty = typeof(T).GetProperties()
                .FirstOrDefault(prop => Attribute.IsDefined(prop, typeof(OrderByAttribute)))
                ?.Name ?? selectProperties.FirstOrDefault();

            var List = await SLConnectionManager.Connection()
                .Request(serviceLayerObjectName)
                .Select(selectClause)
                .OrderBy(orderByProperty)
                .WithPageSize(100)
                .GetAllAsync<T>();
            return List;
        }

        public async Task<T> GetByIdAsync(dynamic id)
        {
            var serviceLayerObjectName = typeof(T).GetCustomAttribute<ServiceLayerObjectNameAttribute>()?.Name;
            if (string.IsNullOrEmpty(serviceLayerObjectName))
            {
                throw new InvalidOperationException("The ServiceLayerObjectName attribute is not defined on the class " + typeof(T).Name);
            }
           
            var item = await SLConnectionManager.Connection()
                .Request(serviceLayerObjectName, id).GetAsync<T>();
            return item;
        }

        public async Task UpdateAsync(T entity)
        {
            var serviceLayerObjectName = typeof(T).GetCustomAttribute<ServiceLayerObjectNameAttribute>()?.Name;
            if (string.IsNullOrEmpty(serviceLayerObjectName))
            {
                throw new InvalidOperationException("The ServiceLayerObjectName attribute is not defined on the class " + typeof(T).Name);
            }
            var PKVal = typeof(T).GetProperties().FirstOrDefault(prop => prop.GetCustomAttributes(typeof(SAPPrimaryKeyAttribute), false).Length > 0)?.GetValue(entity);
            try
            {
                await SLConnectionManager.Connection().Request(serviceLayerObjectName, PKVal).PatchAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create entity: " + ex.Message, ex);
            }
        }
    }
}
