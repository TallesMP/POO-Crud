using MySql.Data.MySqlClient;

namespace Scheduling_CRUD
{
    public class DataBaseConnection
    {
        private static MySqlConnection _connection;
        private static readonly string connectionString =
            "server=localhost;" +
            "user=root;" +
            "password=123456;" +
            "database=sistema_agendamentos;" +
            "sslmode=none;" +
            "allowPublicKeyRetrieval=true;";

        private DataBaseConnection() { }

        public static MySqlConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new MySqlConnection(connectionString);
            }

            if (_connection.State != System.Data.ConnectionState.Open)
            {
                _connection.Open();
            }

            return _connection;
        }
    }
}
