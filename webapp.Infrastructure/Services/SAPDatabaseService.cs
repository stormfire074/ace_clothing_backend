using webapp.Application;
using webapp.Domain;
using webapp.Infrastrcture;
using webapp.SharedServices;

namespace SBODeskReact.Infrastrcture.Services
{
    public class SAPDatabaseService : Service<SAPDatabases>
    {
        public SAPDatabaseService(IRepository<SAPDatabases> repository) : base(repository)
        {
        }

        public override Task<SAPDatabases> AddAsync(SAPDatabases entity)
        {
            if (entity is { SAPPassword: not null, DBPassword: not null })
            {
                entity.DBPassword = entity.DBPassword.EncryptString();
                entity.SAPPassword = entity.SAPPassword.EncryptString();
            }
            
            return base.AddAsync(entity);
        }
        public override async Task<SAPDatabases> GetByIdAsync(int id)
        {
            var entity = await base.GetByIdAsync(id); 
            entity.DBPassword = entity.DBPassword.DecryptString(); 
            entity.SAPPassword = entity.SAPPassword.DecryptString();
            return entity; 
        }
        public override async Task<IReadOnlyList<SAPDatabases>> GetAllAsync()
        {
            var entities = await base.GetAllAsync(); 

            foreach (var entity in entities)
            {
                entity.DBPassword = entity.DBPassword.DecryptString();
                entity.SAPPassword = entity.SAPPassword.DecryptString();
            }

            return entities;
        }


    }
}
