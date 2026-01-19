using MySqlConnector;
using BankNode.Config;

namespace BankNode.Data;

public class MySqlDb
{
    private readonly string _connectionString;

    public MySqlDb(DatabaseConfig cfg)
    {
        _connectionString = cfg.ConnectionString;
    }

    public MySqlConnection Open()
    {
        var conn = new MySqlConnection(_connectionString);
        conn.Open();
        return conn;
    }
}