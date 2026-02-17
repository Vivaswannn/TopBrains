using System;
using System.Collections.Generic;
using System.Linq;

namespace StockPortfolioSystem
{
    public class InvalidTradeException : Exception
    {
        public InvalidTradeException(string message) : base(message) { }
    }

    public class Investor
    {
        public int Id { get; }
        public string Name { get; }

        public Investor(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Id} - {Name}";
        }
    }

    public class Stock
    {
        public string Symbol { get; }
        public string Name { get; }

        private decimal _price;
        public decimal Price
        {
            get => _price;
            private set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPriceChanged(value);
                }
            }
        }

        public event Action<Stock, decimal> PriceChanged;

        public Stock(string symbol, string name, decimal price)
        {
            Symbol = symbol;
            Name = name;
            _price = price;
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice <= 0) return;
            Price = newPrice;
        }

        protected virtual void OnPriceChanged(decimal newPrice)
        {
            PriceChanged?.Invoke(this, newPrice);
        }

        public override string ToString()
        {
            return $"{Symbol} - {Name} - Price: {Price:C}";
        }
    }

    public enum TransactionType
    {
        Buy,
        Sell
    }

    public class Transaction
    {
        public int Id { get; }
        public Investor Investor { get; }
        public Stock Stock { get; }
        public int Quantity { get; }
        public decimal Price { get; }
        public DateTime Date { get; }
        public TransactionType Type { get; }

        public Transaction(int id, Investor investor, Stock stock, int quantity, decimal price, DateTime date, TransactionType type)
        {
            Id = id;
            Investor = investor;
            Stock = stock;
            Quantity = quantity;
            Price = price;
            Date = date;
            Type = type;
        }

        public decimal Value => Quantity * Price * (Type == TransactionType.Buy ? -1 : 1);

        public override string ToString()
        {
            return $"{Id} - {Investor.Name} - {Type} {Quantity} of {Stock.Symbol} @ {Price:C} on {Date:d}";
        }
    }

    public interface IRiskStrategy
    {
        string Name { get; }
        string CalculateRisk(Portfolio portfolio);
    }

    public class ConservativeRiskStrategy : IRiskStrategy
    {
        public string Name => "Conservative";

        public string CalculateRisk(Portfolio portfolio)
        {
            decimal total = portfolio.GetCurrentValue();
            int holdings = portfolio.Holdings.Count;
            if (total < 50000m && holdings >= 5) return "Low";
            if (total < 200000m) return "Medium";
            return "High";
        }
    }

    public class AggressiveRiskStrategy : IRiskStrategy
    {
        public string Name => "Aggressive";

        public string CalculateRisk(Portfolio portfolio)
        {
            decimal total = portfolio.GetCurrentValue();
            int holdings = portfolio.Holdings.Count;
            if (holdings <= 2) return "Very High";
            if (total > 200000m) return "High";
            return "Medium";
        }
    }

    public class Portfolio
    {
        public Investor Investor { get; }
        public Dictionary<string, int> Holdings { get; } = new Dictionary<string, int>();
        public List<Transaction> Transactions { get; } = new List<Transaction>();
        public IRiskStrategy RiskStrategy { get; private set; }

        public Portfolio(Investor investor, IRiskStrategy strategy)
        {
            Investor = investor;
            RiskStrategy = strategy;
        }

        public void SetRiskStrategy(IRiskStrategy strategy)
        {
            RiskStrategy = strategy;
        }

        public void ApplyTransaction(Transaction tx)
        {
            if (tx.Date > DateTime.Now)
            {
                throw new InvalidTradeException("Transaction date cannot be in the future.");
            }

            int sign = tx.Type == TransactionType.Buy ? 1 : -1;
            if (!Holdings.ContainsKey(tx.Stock.Symbol))
            {
                Holdings[tx.Stock.Symbol] = 0;
            }

            if (tx.Type == TransactionType.Sell && Holdings[tx.Stock.Symbol] < tx.Quantity)
            {
                throw new InvalidTradeException("Cannot sell more shares than owned.");
            }

            Holdings[tx.Stock.Symbol] += sign * tx.Quantity;
            Transactions.Add(tx);
        }

        public decimal GetCurrentValue()
        {
            decimal total = 0;
            foreach (var h in Holdings)
            {
                var stock = Market.GetStock(h.Key);
                total += h.Value * stock.Price;
            }
            return total;
        }

        public decimal GetNetProfitLoss()
        {
            return Transactions.Aggregate(0m, (sum, t) => sum + t.Value);
        }

        public string GetRiskLevel()
        {
            return RiskStrategy.CalculateRisk(this);
        }

        public override string ToString()
        {
            return $"{Investor.Name} - Value: {GetCurrentValue():C} - P/L: {GetNetProfitLoss():C} - Risk: {GetRiskLevel()}";
        }
    }

    public static class Market
    {
        public static List<Investor> Investors { get; } = new List<Investor>();
        public static List<Stock> Stocks { get; } = new List<Stock>();
        public static List<Transaction> Transactions { get; } = new List<Transaction>();
        public static Dictionary<string, List<Transaction>> TransactionsByStock { get; } = new Dictionary<string, List<Transaction>>();
        public static Dictionary<int, Portfolio> Portfolios { get; } = new Dictionary<int, Portfolio>();

        private static int _nextTransactionId = 1;

        public static void Seed()
        {
            if (Investors.Any()) return;

            var inv1 = new Investor(1, "Ramesh");
            var inv2 = new Investor(2, "Sneha");
            var inv3 = new Investor(3, "Vikas");

            Investors.Add(inv1);
            Investors.Add(inv2);
            Investors.Add(inv3);

            var s1 = new Stock("INFY", "Infosys", 1500m);
            var s2 = new Stock("TCS", "TCS", 3500m);
            var s3 = new Stock("RELI", "Reliance", 2500m);

            s1.PriceChanged += OnStockPriceChanged;
            s2.PriceChanged += OnStockPriceChanged;
            s3.PriceChanged += OnStockPriceChanged;

            Stocks.Add(s1);
            Stocks.Add(s2);
            Stocks.Add(s3);

            Portfolios[inv1.Id] = new Portfolio(inv1, new ConservativeRiskStrategy());
            Portfolios[inv2.Id] = new Portfolio(inv2, new AggressiveRiskStrategy());
            Portfolios[inv3.Id] = new Portfolio(inv3, new ConservativeRiskStrategy());

            ExecuteTrade(inv1.Id, "INFY", 100, 1400m, DateTime.Now.AddDays(-30), TransactionType.Buy);
            ExecuteTrade(inv1.Id, "TCS", 50, 3200m, DateTime.Now.AddDays(-20), TransactionType.Buy);
            ExecuteTrade(inv1.Id, "INFY", 40, 1600m, DateTime.Now.AddDays(-5), TransactionType.Sell);

            ExecuteTrade(inv2.Id, "RELI", 80, 2300m, DateTime.Now.AddDays(-25), TransactionType.Buy);
            ExecuteTrade(inv2.Id, "INFY", 60, 1500m, DateTime.Now.AddDays(-10), TransactionType.Buy);
            ExecuteTrade(inv2.Id, "RELI", 30, 2600m, DateTime.Now.AddDays(-3), TransactionType.Sell);

            ExecuteTrade(inv3.Id, "TCS", 40, 3100m, DateTime.Now.AddDays(-15), TransactionType.Buy);
            ExecuteTrade(inv3.Id, "INFY", 30, 1450m, DateTime.Now.AddDays(-7), TransactionType.Buy);
        }

        private static void OnStockPriceChanged(Stock stock, decimal newPrice)
        {
            Console.WriteLine($"Price changed: {stock.Symbol} -> {newPrice:C}");
        }

        public static Stock GetStock(string symbol)
        {
            var s = Stocks.FirstOrDefault(x => x.Symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase));
            if (s == null) throw new ArgumentException("Stock not found.");
            return s;
        }

        public static Portfolio GetPortfolio(int investorId)
        {
            if (!Portfolios.TryGetValue(investorId, out var p))
                throw new ArgumentException("Portfolio not found.");
            return p;
        }

        public static Transaction ExecuteTrade(int investorId, string symbol, int quantity, decimal price, DateTime date, TransactionType type)
        {
            if (date > DateTime.Now)
                throw new InvalidTradeException("Transaction date cannot be in the future.");

            var investor = Investors.FirstOrDefault(i => i.Id == investorId);
            if (investor == null) throw new ArgumentException("Investor not found.");

            var stock = GetStock(symbol);

            var tx = new Transaction(_nextTransactionId++, investor, stock, quantity, price, date, type);
            var portfolio = GetPortfolio(investorId);

            portfolio.ApplyTransaction(tx);

            Transactions.Add(tx);
            if (!TransactionsByStock.ContainsKey(symbol))
                TransactionsByStock[symbol] = new List<Transaction>();
            TransactionsByStock[symbol].Add(tx);

            return tx;
        }

        public static Investor GetMostProfitableInvestor()
        {
            var query = Portfolios.Values
                .Select(p => new { Investor = p.Investor, Profit = p.GetNetProfitLoss() })
                .OrderByDescending(x => x.Profit)
                .FirstOrDefault();
            return query?.Investor;
        }

        public static Stock GetHighestVolumeStock()
        {
            var query = Transactions
                .GroupBy(t => t.Stock)
                .Select(g => new { Stock = g.Key, Volume = g.Sum(t => t.Quantity) })
                .OrderByDescending(x => x.Volume)
                .FirstOrDefault();
            return query?.Stock;
        }

        public static ILookup<Stock, Transaction> GroupTransactionsByStock()
        {
            return Transactions.ToLookup(t => t.Stock);
        }

        public static decimal GetNetProfitLossAll()
        {
            return Transactions.Aggregate(0m, (sum, t) => sum + t.Value);
        }

        public static IEnumerable<Investor> GetInvestorsWithNegativeReturns()
        {
            return Portfolios.Values
                .Where(p => p.GetNetProfitLoss() < 0)
                .Select(p => p.Investor);
        }
    }

    public class Program
    {
        public static void Main()
        {
            Market.Seed();
            RunMenu();
        }

        private static void RunMenu()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("=== Stock Portfolio Management System ===");
                Console.WriteLine("1. List investors and portfolios");
                Console.WriteLine("2. List stocks");
                Console.WriteLine("3. Execute trade");
                Console.WriteLine("4. Update stock price");
                Console.WriteLine("5. Show most profitable investor");
                Console.WriteLine("6. Show stock with highest volume");
                Console.WriteLine("7. Group transactions by stock");
                Console.WriteLine("8. Show net profit/loss (all)");
                Console.WriteLine("9. Show investors with negative returns");
                Console.WriteLine("0. Exit");
                Console.Write("Choose: ");

                var choice = Console.ReadLine();
                Console.WriteLine();

                if (choice == "0") break;

                try
                {
                    switch (choice)
                    {
                        case "1": ListPortfolios(); break;
                        case "2": ListStocks(); break;
                        case "3": ExecuteTradeFromUser(); break;
                        case "4": UpdatePriceFromUser(); break;
                        case "5": ShowMostProfitableInvestor(); break;
                        case "6": ShowHighestVolumeStock(); break;
                        case "7": ShowTransactionsByStock(); break;
                        case "8": ShowNetProfitLossAll(); break;
                        case "9": ShowNegativeInvestors(); break;
                        default: Console.WriteLine("Invalid option."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        private static int ReadInt(string msg)
        {
            Console.Write(msg);
            return int.Parse(Console.ReadLine() ?? "0");
        }

        private static decimal ReadDecimal(string msg)
        {
            Console.Write(msg);
            return decimal.Parse(Console.ReadLine() ?? "0");
        }

        private static DateTime ReadDateTime(string msg)
        {
            Console.Write(msg);
            return DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString("g"));
        }

        private static void ListPortfolios()
        {
            foreach (var p in Market.Portfolios.Values)
                Console.WriteLine(p);
        }

        private static void ListStocks()
        {
            foreach (var s in Market.Stocks)
                Console.WriteLine(s);
        }

        private static void ExecuteTradeFromUser()
        {
            foreach (var inv in Market.Investors)
                Console.WriteLine(inv);
            int investorId = ReadInt("Investor id: ");

            ListStocks();
            Console.Write("Stock symbol: ");
            string symbol = Console.ReadLine() ?? "";

            int qty = ReadInt("Quantity: ");
            decimal price = ReadDecimal("Price: ");
            DateTime date = ReadDateTime("Date (e.g. 2026-02-17): ");

            Console.Write("Type (B=Buy, S=Sell): ");
            string t = Console.ReadLine() ?? "B";
            TransactionType type = t.Equals("S", StringComparison.OrdinalIgnoreCase)
                ? TransactionType.Sell
                : TransactionType.Buy;

            var tx = Market.ExecuteTrade(investorId, symbol, qty, price, date, type);
            Console.WriteLine("Trade executed: " + tx);
        }

        private static void UpdatePriceFromUser()
        {
            ListStocks();
            Console.Write("Stock symbol: ");
            string symbol = Console.ReadLine() ?? "";
            decimal price = ReadDecimal("New price: ");
            var stock = Market.GetStock(symbol);
            stock.UpdatePrice(price);
        }

        private static void ShowMostProfitableInvestor()
        {
            var inv = Market.GetMostProfitableInvestor();
            if (inv == null) Console.WriteLine("No data.");
            else Console.WriteLine("Most profitable investor: " + inv.Name);
        }

        private static void ShowHighestVolumeStock()
        {
            var stock = Market.GetHighestVolumeStock();
            if (stock == null) Console.WriteLine("No data.");
            else Console.WriteLine("Highest volume stock: " + stock);
        }

        private static void ShowTransactionsByStock()
        {
            Console.WriteLine("Transactions grouped by stock:");
            var groups = Market.GroupTransactionsByStock();
            foreach (var g in groups)
            {
                Console.WriteLine(g.Key.Symbol + ":");
                foreach (var t in g)
                    Console.WriteLine("  " + t);
            }
        }

        private static void ShowNetProfitLossAll()
        {
            Console.WriteLine("Net profit/loss (all): " + Market.GetNetProfitLossAll().ToString("C"));
        }

        private static void ShowNegativeInvestors()
        {
            Console.WriteLine("Investors with negative returns:");
            foreach (var inv in Market.GetInvestorsWithNegativeReturns())
                Console.WriteLine(inv);
        }
    }
}
