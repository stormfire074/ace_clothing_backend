using webapp.Application;
using webapp.Domain;
using webapp.Infrastrcture;
namespace SBODeskReact.Infrastrcture.Services
{
    public class BusinessPartnerService : SAPService<BusinessPartner>
    {
        public BusinessPartnerService(ISAPRepository<BusinessPartner> repository) : base(repository)
        {

        }




    }
}
