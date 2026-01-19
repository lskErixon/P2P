using MySqlConnector;

namespace BankNode.Data;

public sealed class AccountRepository
{
    private readonly MySqlDb _db;

    public AccountRepository(MySqlDb db) => _db = db;

    public void Create(string accountId)
    {
        using var cn = _db.Open();
        using var cmd = cn.CreateCommand();
        cmd.CommandText = """
            INSERT INTO accounts(account_id, balance, created_at)
            VALUES (@id, 0, NOW());
            """;
        cmd.Parameters.AddWithValue("@id", accountId);
        cmd.ExecuteNonQuery();
    }

    public int GetBalance(string accountId)
    {
        using var cn = _db.Open();
        using var cmd = cn.CreateCommand();
        cmd.CommandText = "SELECT balance FROM accounts WHERE account_id=@id;";
        cmd.Parameters.AddWithValue("@id", accountId);

        var obj = cmd.ExecuteScalar();
        if (obj is null) throw new Exception("account not found");
        return Convert.ToInt32(obj);
    }

    public void Deposit(string accountId, int amount)
    {
        using var cn = _db.Open();
        using var cmd = cn.CreateCommand();
        cmd.CommandText = """
            UPDATE accounts
            SET balance = balance + @amt
            WHERE account_id=@id;
            """;
        cmd.Parameters.AddWithValue("@id", accountId);
        cmd.Parameters.AddWithValue("@amt", amount);

        if (cmd.ExecuteNonQuery() != 1) throw new Exception("account not found");
    }

    public void Withdraw(string accountId, int amount)
    {
        // bezpečné odečtení: jen když bude dost peněz
        using var cn = _db.Open();
        using var cmd = cn.CreateCommand();
        cmd.CommandText = """
            UPDATE accounts
            SET balance = balance - @amt
            WHERE account_id=@id AND balance >= @amt;
            """;
        cmd.Parameters.AddWithValue("@id", accountId);
        cmd.Parameters.AddWithValue("@amt", amount);

        if (cmd.ExecuteNonQuery() != 1) throw new Exception("insufficient funds or account not found");
    }

    public void Remove(string accountId)
    {
        using var cn = _db.Open();
        using var cmd = cn.CreateCommand();
        cmd.CommandText = "DELETE FROM accounts WHERE account_id=@id;";
        cmd.Parameters.AddWithValue("@id", accountId);

        if (cmd.ExecuteNonQuery() != 1) throw new Exception("account not found");
    }

    public int TotalAmount()
    {
        using var cn = _db.Open();
        using var cmd = cn.CreateCommand();
        cmd.CommandText = "SELECT COALESCE(SUM(balance), 0) FROM accounts;";
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public int ClientCount()
    {
        using var cn = _db.Open();
        using var cmd = cn.CreateCommand();
        cmd.CommandText = "SELECT COUNT(*) FROM accounts;";
        return Convert.ToInt32(cmd.ExecuteScalar());
    }
}
