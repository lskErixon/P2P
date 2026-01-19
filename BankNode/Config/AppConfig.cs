using System.Text.Json;

namespace BankNode.Config;

public class AppConfig
{
    public string BankName { get; set; }
    public string BankIp { get; set; }
    public int Port { get; set; }
    public DatabaseConfig Database { get; set; }

    public static AppConfig Load(string path)
    {
        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<AppConfig>(json)!;
    }
}

public class DatabaseConfig
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
    public string Database { get; set; }

    public string ConnectionString =>
        $"Server={Host};Port={Port};Database={Database};User={User};Password={Password};";
}