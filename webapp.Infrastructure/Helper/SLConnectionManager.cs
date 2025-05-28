using B1SLayer;

namespace webapp.Infrastructure
{
    public static class SLConnectionManager 
    {
        private static SLConnection _currentConnection;

        public static SLConnection Connection()
        {
            return _currentConnection;
        }

        public static void RemoveConnection()
        {
            _currentConnection = null;
        }

        public static void SetConnection(SLConnection connection)
        {
            _currentConnection = connection;

        }
        public static string GetCurrentDB()
        {
           return _currentConnection.CompanyDB;
        }
    }
}
