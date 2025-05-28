using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webapp.Domain
{
    public class SAPDatabases : IIdentifiable
    {
        public int Id { get; set; }
        
        public String? ServerName { get; set; }

        public String? DBServerType { get; set; }
        
        public String? LicenseServer { get; set; }
        
        public String? CompanyDB { get; set; }
        
        public String? DBUsername { get; set; }
        
        public String? DBPassword { get; set; }
        
        public String? SAPUsername { get; set; }

        public String? SAPPassword { get; set; }

        public String? ODBCServer { get; set; }
        
        public String? ServiceLayerURL { get; set; }
        
        public String? ServiceLayerVersion { get; set; }
        
        public String? DBDriver { get; set; }

        public Int16? CreateUDFs { get; set; }
        
        public Boolean? IsDefault { get; set; }

    }
}
