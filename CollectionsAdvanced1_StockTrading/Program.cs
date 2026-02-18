using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionsAdvanced1_StockTrading
{
    public enum OrderSide { Buy, Sell }

    public interface IOrder<T> where T : IComparable<T>
    {
        string OrderId { get; }
        T Instrument { get; }
        OrderSide Side { get; }
        decimal Price { get; }
        int Quantity { get; }
        DateTime Timestamp { get; }
        int Priority { get; }
    }

    public class StockOrder : IOrder<string>
    {
        public string OrderId { get; set; }
        public string Instrument { get; set; }
        public OrderSide Side { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime Timestamp { get; set; }
        public int Priority { get; set; }
    }

    public class OrderBook<T> where T : IComparable<T>
    {
        private readonly Dictionary<string, IOrder<T>> _allOrders = new Dictionary<string, IOrder<T>>();
        private readonly List<IOrder<T>> _buyOrders = new List<IOrder<T>>();
        private readonly List<IOrder<T>> _sellOrders = new List<IOrder<T>>();
        private readonly List<decimal> _priceHistory = new List<decimal>();

        private static readonly Comparer<IOrder<T>> BuyComparer = Comparer<IOrder<T>>.Create((a, b) =>
        {
            int pc = a.Priority.CompareTo(b.Priority);
            if (pc != 0) return -pc;
            int priceC = b.Price.CompareTo(a.Price);
            if (priceC != 0) return priceC;
            return a.Timestamp.CompareTo(b.Timestamp);
        });

        private static readonly Comparer<IOrder<T>> SellComparer = Comparer<IOrder<T>>.Create((a, b) =>
        {
            int pc = a.Priority.CompareTo(b.Priority);
            if (pc != 0) return -pc;
            int priceC = a.Price.CompareTo(b.Price);
            if (priceC != 0) return priceC;
            return a.Timestamp.CompareTo(b.Timestamp);
        });

        public void AddOrder(IOrder<T> order)
        {
            if (order == null || _allOrders.ContainsKey(order.OrderId)) return;
            _allOrders[order.OrderId] = order;
            if (order.Side == OrderSide.Buy)
            {
                _buyOrders.Add(order);
                _buyOrders.Sort(BuyComparer);
            }
            else
            {
                _sellOrders.Add(order);
                _sellOrders.Sort(SellComparer);
            }
        }

        public IOrder<T> PeekNextBuy() => _buyOrders.FirstOrDefault();
        public IOrder<T> PeekNextSell() => _sellOrders.FirstOrDefault();

        public void RecordPrice(decimal price)
        {
            _priceHistory.Add(price);
            if (_priceHistory.Count > 1000) _priceHistory.RemoveAt(0);
        }

        public decimal CalculateVWAP(int count)
        {
            if (count <= 0 || _priceHistory.Count == 0) return 0;
            var recent = _priceHistory.TakeLast(count);
            return recent.Average();
        }
    }

    class Program
    {
        static void Main()
        {
            var book = new OrderBook<string>();
            book.AddOrder(new StockOrder
            {
                OrderId = "1",
                Instrument = "AAPL",
                Side = OrderSide.Buy,
                Price = 150m,
                Quantity = 100,
                Timestamp = DateTime.Now,
                Priority = 1
            });
            book.RecordPrice(150m);
            book.RecordPrice(151m);
            Console.WriteLine("VWAP(2): " + book.CalculateVWAP(2));
        }
    }
}
