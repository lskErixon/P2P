using BankNode.Config;
using BankNode.Data;

var config = AppConfig.Load("Config/config.json");

var db = new MySqlDb(config.Database);
using var conn = db.Open();

Console.WriteLine("MySQL OK");