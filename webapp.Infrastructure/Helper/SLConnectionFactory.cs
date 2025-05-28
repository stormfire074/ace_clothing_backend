using B1SLayer;

using webapp.Application;
using webapp.Domain;

namespace webapp.Infrastructure
{
    public class SLConnectionFactory : ISLConnectionFactory
    {
        private readonly Dictionary<string, SLConnection> _connections = new Dictionary<string, SLConnection>();
        public SLConnectionFactory(IEnumerable<ConnectionInfo> connectionInfos)
        {
            foreach (var info in connectionInfos)
            {
                _connections[info.Name] = new SLConnection(info.Url, info.Database, info.Username, info.Password);
            }
        }

        public SLConnection GetConnection(string name)
        {
            if (_connections.TryGetValue(name, out var connection))
            {
                return connection;
            }
            throw new ArgumentException("No connection found with the given name.", nameof(name));
        }
        public async Task<(SLConnection SLConnection,SLLoginResponse Response)> Login(Login login)
        {
            if (_connections.TryGetValue(login.CompanyDB, out var connection))
            { 
                var _connection = new SLConnection(connection.ServiceLayerRoot, login.CompanyDB, login.Username, login.Password);
                var loginresponse=await _connection.LoginAsync();
                SLConnectionManager.SetConnection(_connection);
                return (_connection, loginresponse);

            }
            throw new ArgumentException("No connection found with the given name.", nameof(login.CompanyDB));
        }
        public async Task<bool> Logout() 
        { 
            var connection= SLConnectionManager.Connection();
            try
            {
                await connection.LogoutAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            SLConnectionManager.RemoveConnection();
            return true;
        }

    }
}
