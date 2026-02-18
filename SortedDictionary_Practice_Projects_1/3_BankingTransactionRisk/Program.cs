using BankingTransactionRisk.Domain;
using BankingTransactionRisk.Exceptions;
using BankingTransactionRisk.Services;

var service = new RiskMonitoringService();

var t1 = new OnlineTransaction("T1", 50, "192.168.1.1");
var t2 = new WireTransfer("T2", 500, "SWIFT123");
service.AddTransaction(t1);
service.AddTransaction(t2);

Console.WriteLine("Transactions by risk (descending):");
foreach (var kv in service.GetTransactionsByRisk())
    foreach (var t in kv.Value)
        Console.WriteLine($"  Risk {kv.Key}: {t.GetTransactionType()} {t.Id} Amount={t.Amount}");

try { service.ValidateAmount(-10); }
catch (NegativeAmountException ex) { Console.WriteLine($"\nExpected: {ex.Message}"); }

Console.WriteLine("Done.");
