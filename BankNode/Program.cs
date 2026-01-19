using BankNode.Config;
using BankNode.Data;

var cfg = AppConfig.Load("Config/config.json");

var db = new MySqlDb(cfg.Database);
using var cn = db.Open();

using var cmd = cn.CreateCommand();
cmd.CommandText = "SELECT 1;";
var result = cmd.ExecuteScalar();

Console.WriteLine($"MySQL OK, SELECT 1 = {result}");