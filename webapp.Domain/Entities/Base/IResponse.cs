using System.Collections;

namespace Domain.Entities
{
    public class Error
    {
        public Error(Exception ex)
        {
            Code = ex.GetType().Name;
            Message = ex.Message;

        }
        public string Code { get; set; }
        public string Message { get; set; }
    }
    public class ServiceResponse
    {
        public Error Error { get; set; }
    }
    public class SaveResponse : ServiceResponse
    {
        public SaveResponse(bool isSaved = false, Exception exception = null)
        {
            IsSuccess = isSaved;
            if (exception != null)
            {
                Error = new Error(exception);
            }

        }

        public bool IsSuccess { get; set; }

    }
    public interface IListResponse
    {
        IList Entities { get; }
        int TotalCount { get; }
        int PageNumber { get; }
        int PageSize { get; }
    }
    public class ListResponse<T> : ServiceResponse, IListResponse
    {
        public ListResponse(List<T> entities = null, ListRequest request = null, long totalCount = 0, Exception exception = null)
        {
            Entities = entities;
            TotalCount = Convert.ToInt32(totalCount);
            PageNumber = request.PageNumber;
            PageSize = request.PageSize;
            if (exception != null)
            {
                Error = new Error(exception);
            }
        }
        IList IListResponse.Entities => Entities;
        public List<T> Entities { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public interface IRetrieveResponse
    {
        object Entity { get; }
    }

    public class RetrieveResponse<T> : ServiceResponse, IRetrieveResponse
    {
        public RetrieveResponse(T entity, Exception exception = null)
        {
            Entity = entity;
            if (exception != null)
            {
                Error = new Error(exception);
            }
        }
        public T Entity { get; set; }
        object IRetrieveResponse.Entity => Entity;


    }

    public class DeleteReponse : ServiceResponse
    {
        public DeleteReponse(bool isDeleted, Exception exception = null)
        {
            IsDeleted = false;
            if (exception != null)
            {
                Error = new Error(exception);
            }
            else
            {
                IsDeleted = isDeleted;
            }
        }
        public bool IsDeleted { get; set; }

    }
}
