using BankNode.Data;

namespace BankNode.Services;

public sealed class BankService
{
    private readonly string _bankIp;
    private readonly AccountRepository _repo;

    public BankService(string bankIp, AccountRepository repo)
    {
        _bankIp = bankIp;
        _repo = repo;
    }

    public string BankCode() => $"BC {_bankIp}";

    public string CreateAccount()
    {
        var id = Guid.NewGuid().ToString("N");
        _repo.Create(id);
        return $"AC {id}/{_bankIp}";
    }

    public string Balance(string accIp)
    {
        var (acc, ip) = ParseAccIp(accIp);
        EnsureLocal(ip);
        return $"AB {_repo.GetBalance(acc)}";
    }

    public string Deposit(string accIp, int amount)
    {
        if (amount <= 0) throw new Exception("amount must be > 0");
        var (acc, ip) = ParseAccIp(accIp);
        EnsureLocal(ip);
        _repo.Deposit(acc, amount);
        return "AD";
    }

    public string Withdraw(string accIp, int amount)
    {
        if (amount <= 0) throw new Exception("amount must be > 0");
        var (acc, ip) = ParseAccIp(accIp);
        EnsureLocal(ip);
        _repo.Withdraw(acc, amount);
        return "AW";
    }

    public string Remove(string accIp)
    {
        var (acc, ip) = ParseAccIp(accIp);
        EnsureLocal(ip);
        _repo.Remove(acc);
        return "AR";
    }

    public string BankTotal() => $"BA {_repo.TotalAmount()}";
    public string BankClients() => $"BN {_repo.ClientCount()}";

    private void EnsureLocal(string ip)
    {
        if (ip != _bankIp) throw new Exception("wrong bank ip");
    }

    private static (string acc, string ip) ParseAccIp(string value)
    {
        var idx = value.IndexOf('/');
        if (idx <= 0 || idx == value.Length - 1) throw new Exception("invalid account/ip format");
        return (value[..idx], value[(idx + 1)..]);
    }
}