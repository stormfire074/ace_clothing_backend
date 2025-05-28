using B1SLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webapp.Domain;

namespace webapp.Application
{
    public interface ISLConnectionFactory
    {
        B1SLayer.SLConnection GetConnection(string name);
        Task<(B1SLayer.SLConnection SLConnection, SLLoginResponse Response)> Login(Login login);
        Task<bool> Logout();


    }
}
