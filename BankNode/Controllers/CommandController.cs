using BankNode.Services;

namespace BankNode.Controllers;

public sealed class CommandController
{
    private readonly BankService _svc;

    public CommandController(BankService svc) => _svc = svc;

    public string Handle(string input)
    {
        try
        {
            input = (input ?? "").Trim();
            if (input.Length == 0) return "ER empty command";

            var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var cmd = parts[0];

            return cmd switch
            {
                "BC" when parts.Length == 1 => _svc.BankCode(),
                "AC" when parts.Length == 1 => _svc.CreateAccount(),
                "AB" when parts.Length == 2 => _svc.Balance(parts[1]),
                "AD" when parts.Length == 3 => _svc.Deposit(parts[1], ParseInt(parts[2])),
                "AW" when parts.Length == 3 => _svc.Withdraw(parts[1], ParseInt(parts[2])),
                "AR" when parts.Length == 2 => _svc.Remove(parts[1]),
                "BA" when parts.Length == 1 => _svc.BankTotal(),
                "BN" when parts.Length == 1 => _svc.BankClients(),
                _ => "ER unknown or invalid command format"
            };
        }
        catch (Exception ex)
        {
            return $"ER {ex.Message}";
        }
    }

    private static int ParseInt(string s)
    {
        if (!int.TryParse(s, out var n)) throw new Exception("invalid number");
        return n;
    }
}