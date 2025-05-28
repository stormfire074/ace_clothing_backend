using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public interface ISaveRequest
    {
        object EntityId { get; set; }
        object Entity { get; set; }
    }

    public class SaveRequest<TEntity> : ISaveRequest
    {
        public object EntityId { get; set; }
        public TEntity Entity { get; set; }
        object ISaveRequest.Entity
        {
            get { return Entity; }
            set { Entity = (TEntity)value; }
        }
    }

    public class ListRequest
    {
        public ListRequest() { }
        public ListRequest(int pageNumber,int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        public int Skip { get { return (PageNumber - 1) * PageSize; } }

        public int Take { get { return PageSize; } }
    }
    public class RetrieveRequest
    {
        public object EntityId { get; set; }
    }
}
