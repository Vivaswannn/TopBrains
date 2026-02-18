using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericsScenario4_Financial
{
    public interface IFinancialInstrument
    {
        string Symbol { get; }
        decimal CurrentPrice { get; }
        InstrumentType Type { get; }
    }

    public enum InstrumentType { Stock, Bond, Option, Future }

    public enum Trend { Upward, Downward, Sideways }

    public class Portfolio<T> where T : IFinancialInstrument
    {
        private readonly Dictionary<T, int> _holdings = new();
        private readonly Dictionary<T, decimal> _purchasePrices = new();

        public void Buy(T instrument, int quantity, decimal price)
        {
            if (quantity <= 0 || price <= 0)
                throw new ArgumentException("Quantity and price must be positive.");
            if (_holdings.ContainsKey(instrument))
            {
                var oldQ = _holdings[instrument];
                var oldP = _purchasePrices[instrument];
                _holdings[instrument] = oldQ + quantity;
                _purchasePrices[instrument] = (oldP * oldQ + price * quantity) / (oldQ + quantity);
            }
            else
            {
                _holdings[instrument] = quantity;
                _purchasePrices[instrument] = price;
            }
        }

        public decimal? Sell(T instrument, int quantity, decimal currentPrice)
        {
            if (!_holdings.TryGetValue(instrument, out var held) || held < quantity)
                return null;
            _holdings[instrument] = held - quantity;
            if (_holdings[instrument] == 0)
            {
                _holdings.Remove(instrument);
                _purchasePrices.Remove(instrument);
            }
            return quantity * currentPrice;
        }

        public decimal CalculateTotalValue()
        {
            return _holdings.Sum(kv => kv.Value * kv.Key.CurrentPrice);
        }

        public (T instrument, decimal returnPercentage)? GetTopPerformer(Dictionary<T, decimal> purchasePrices)
        {
            T top = default;
            decimal topReturn = decimal.MinValue;
            foreach (var kv in _holdings)
            {
                if (!purchasePrices.TryGetValue(kv.Key, out var buyPrice)) continue;
                var ret = buyPrice == 0 ? 0 : (kv.Key.CurrentPrice - buyPrice) / buyPrice * 100;
                if (ret > topReturn)
                {
                    topReturn = ret;
                    top = kv.Key;
                }
            }
            return top != null ? (top, topReturn) : null;
        }

        public IReadOnlyDictionary<T, int> Holdings => _holdings;
    }

    public class Stock : IFinancialInstrument
    {
        public string Symbol { get; set; }
        public decimal CurrentPrice { get; set; }
        public InstrumentType Type => InstrumentType.Stock;
        public string CompanyName { get; set; }
        public decimal DividendYield { get; set; }
    }

    public class Bond : IFinancialInstrument
    {
        public string Symbol { get; set; }
        public decimal CurrentPrice { get; set; }
        public InstrumentType Type => InstrumentType.Bond;
        public DateTime MaturityDate { get; set; }
        public decimal CouponRate { get; set; }
    }

    public class TradingStrategy<T> where T : IFinancialInstrument
    {
        public void Execute(Portfolio<T> portfolio, Func<T, bool> buyCondition, Func<T, bool> sellCondition)
        {
            foreach (var kv in portfolio.Holdings.ToList())
            {
                if (sellCondition(kv.Key))
                    portfolio.Sell(kv.Key, kv.Value, kv.Key.CurrentPrice);
            }
        }

        public Dictionary<string, decimal> CalculateRiskMetrics(IEnumerable<T> instruments)
        {
            var list = instruments.ToList();
            var prices = list.Select(i => (double)i.CurrentPrice).ToArray();
            if (prices.Length == 0) return new Dictionary<string, decimal>();
            var avg = prices.Average();
            var variance = prices.Select(p => (p - avg) * (p - avg)).Average();
            var volatility = (decimal)Math.Sqrt(variance);
            var beta = volatility != 0 ? volatility / 10 : 0;
            var sharpe = volatility != 0 ? (decimal)(avg / Math.Sqrt(variance)) : 0;
            return new Dictionary<string, decimal>
            {
                ["Volatility"] = volatility,
                ["Beta"] = beta,
                ["SharpeRatio"] = sharpe
            };
        }
    }

    public class PriceHistory<T> where T : IFinancialInstrument
    {
        private readonly Dictionary<T, List<(DateTime date, decimal price)>> _history = new();

        public void AddPrice(T instrument, DateTime timestamp, decimal price)
        {
            if (!_history.ContainsKey(instrument))
                _history[instrument] = new List<(DateTime, decimal)>();
            _history[instrument].Add((timestamp, price));
        }

        public decimal? GetMovingAverage(T instrument, int days)
        {
            if (!_history.TryGetValue(instrument, out var list) || list.Count < days)
                return null;
            var recent = list.OrderByDescending(x => x.date).Take(days).Select(x => x.price).ToList();
            return recent.Average();
        }

        public Trend DetectTrend(T instrument, int period)
        {
            if (!_history.TryGetValue(instrument, out var list) || list.Count < period)
                return Trend.Sideways;
            var ordered = list.OrderBy(x => x.date).ToList();
            var first = ordered.Take(period / 2).Average(x => x.price);
            var last = ordered.TakeLast(period / 2).Average(x => x.price);
            if (last > first * 1.01m) return Trend.Upward;
            if (last < first * 0.99m) return Trend.Downward;
            return Trend.Sideways;
        }
    }

    class Program
    {
        static void Main()
        {
            var stock1 = new Stock { Symbol = "AAPL", CurrentPrice = 150m, CompanyName = "Apple" };
            var stock2 = new Stock { Symbol = "MSFT", CurrentPrice = 380m, CompanyName = "Microsoft" };
            var bond1 = new Bond { Symbol = "GOV1", CurrentPrice = 98m, MaturityDate = DateTime.Now.AddYears(5), CouponRate = 2.5m };

            var portfolio = new Portfolio<IFinancialInstrument>();
            portfolio.Buy(stock1, 10, 140m);
            portfolio.Buy(stock2, 5, 350m);
            portfolio.Buy(bond1, 100, 99m);

            Console.WriteLine("Total value: " + portfolio.CalculateTotalValue().ToString("C"));
            var purchasePrices = new Dictionary<IFinancialInstrument, decimal> { [stock1] = 140m, [stock2] = 350m, [bond1] = 99m };
            var top = portfolio.GetTopPerformer(purchasePrices);
            if (top.HasValue) Console.WriteLine($"Top performer: {top.Value.instrument.Symbol} - Return: {top.Value.returnPercentage:F1}%");

            var history = new PriceHistory<Stock>();
            history.AddPrice(stock1, DateTime.Now.AddDays(-5), 145m);
            history.AddPrice(stock1, DateTime.Now.AddDays(-3), 148m);
            history.AddPrice(stock1, DateTime.Now, 150m);
            Console.WriteLine("Trend: " + history.DetectTrend(stock1, 3));

            var strategy = new TradingStrategy<IFinancialInstrument>();
            var metrics = strategy.CalculateRiskMetrics(new[] { stock1, stock2 });
            foreach (var m in metrics)
                Console.WriteLine($"{m.Key}: {m.Value:F2}");
        }
    }
}
