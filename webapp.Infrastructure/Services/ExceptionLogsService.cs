using Domain.Entities;
using webapp.Application;
namespace webapp.Infrastrcture
{
    public class ExceptionLogsService : Service<ExceptionLogs>
    {
        public ExceptionLogsService(IRepository<ExceptionLogs> repository) : base(repository)
        {
        }
    }
}
