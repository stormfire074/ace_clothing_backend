using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace webapp.Application
{
    public interface ISLConnectionManager
    {
        B1SLayer.SLConnection Connection();
        void SetConnection(B1SLayer.SLConnection connection);
        void RemoveConnection();
    }
}
