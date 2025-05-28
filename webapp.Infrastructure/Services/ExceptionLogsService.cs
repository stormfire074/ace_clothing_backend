using webapp.Application;
using webapp.Domain;
namespace webapp.Infrastrcture
{
    public class ExceptionLogsService : Service<ExceptionLogs>
    {
        public ExceptionLogsService(IRepository<ExceptionLogs> repository) : base(repository)
        {
        }
    }
}
