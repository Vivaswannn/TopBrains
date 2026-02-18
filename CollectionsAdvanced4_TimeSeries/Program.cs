using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionsAdvanced4_TimeSeries
{
    public struct TimeSeriesPoint<TTimestamp, TValue>
    {
        public TTimestamp Timestamp { get; set; }
        public TValue Value { get; set; }
    }

    public class TimeSeriesDatabase<TTimestamp, TValue>
        where TTimestamp : IComparable<TTimestamp>, IEquatable<TTimestamp>
        where TValue : struct
    {
        private readonly List<TTimestamp> _timestamps = new List<TTimestamp>();
        private readonly List<TValue> _values = new List<TValue>();

        public void Append(TimeSeriesPoint<TTimestamp, TValue> point)
        {
            _timestamps.Add(point.Timestamp);
            _values.Add(point.Value);
        }

        public void AppendRange(IEnumerable<TimeSeriesPoint<TTimestamp, TValue>> points)
        {
            foreach (var p in points)
                Append(p);
        }

        public IEnumerable<TimeSeriesPoint<TTimestamp, TValue>> Query(TTimestamp start, TTimestamp end)
        {
            for (int i = 0; i < _timestamps.Count; i++)
            {
                var t = _timestamps[i];
                if (t.CompareTo(start) >= 0 && t.CompareTo(end) <= 0)
                    yield return new TimeSeriesPoint<TTimestamp, TValue> { Timestamp = t, Value = _values[i] };
            }
        }

        public double? RollingAverage(TTimestamp start, TTimestamp end, int windowSize)
        {
            var inRange = new List<TValue>();
            for (int i = 0; i < _timestamps.Count; i++)
            {
                var t = _timestamps[i];
                if (t.CompareTo(start) >= 0 && t.CompareTo(end) <= 0)
                    inRange.Add(_values[i]);
            }
            if (inRange.Count < windowSize) return null;
            var window = inRange.TakeLast(windowSize);
            return window.Select(v => Convert.ToDouble(v)).Average();
        }

        public int Count => _timestamps.Count;
    }

    class Program
    {
        static void Main()
        {
            var db = new TimeSeriesDatabase<DateTime, double>();
            db.Append(new TimeSeriesPoint<DateTime, double> { Timestamp = DateTime.Now.AddHours(-2), Value = 10.5 });
            db.Append(new TimeSeriesPoint<DateTime, double> { Timestamp = DateTime.Now.AddHours(-1), Value = 11.0 });
            db.Append(new TimeSeriesPoint<DateTime, double> { Timestamp = DateTime.Now, Value = 12.0 });

            var start = DateTime.Now.AddHours(-3);
            var end = DateTime.Now.AddMinutes(1);
            var points = db.Query(start, end).ToList();
            Console.WriteLine("Points in range: " + points.Count);
            Console.WriteLine("Rolling avg(2): " + db.RollingAverage(start, end, 2));
        }
    }
}
