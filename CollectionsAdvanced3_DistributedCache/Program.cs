using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionsAdvanced3_DistributedCache
{
    public class CacheEntry<T>
    {
        public T Value { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }

    public class DistributedCache<TKey, TValue>
        where TKey : IEquatable<TKey>
        where TValue : class
    {
        private readonly Dictionary<uint, string> _hashRing = new Dictionary<uint, string>();
        private readonly Dictionary<TKey, CacheEntry<TValue>> _localCache = new Dictionary<TKey, CacheEntry<TValue>>();
        private readonly List<uint> _sortedKeys = new List<uint>();
        private static readonly Random Rnd = new Random();

        public void AddNode(string nodeId)
        {
            uint hash = (uint)nodeId.GetHashCode();
            _hashRing[hash] = nodeId;
            _sortedKeys.Add(hash);
            _sortedKeys.Sort();
        }

        private string GetNodeForHash(uint keyHash)
        {
            if (_sortedKeys.Count == 0) return null;
            foreach (var k in _sortedKeys)
                if (k >= keyHash)
                    return _hashRing[k];
            return _hashRing[_sortedKeys[0]];
        }

        public void Set(TKey key, TValue value, TimeSpan? ttl = null)
        {
            var entry = new CacheEntry<TValue>
            {
                Value = value,
                ExpiresAt = ttl.HasValue ? DateTime.Now.Add(ttl.Value) : null
            };
            _localCache[key] = entry;
        }

        public TValue Get(TKey key)
        {
            if (!_localCache.TryGetValue(key, out var entry)) return null;
            if (entry.ExpiresAt.HasValue && entry.ExpiresAt.Value < DateTime.Now)
            {
                _localCache.Remove(key);
                return null;
            }
            return entry.Value;
        }

        public string GetNodeForKey(TKey key)
        {
            uint hash = (uint)key.GetHashCode();
            return GetNodeForHash(hash);
        }
    }

    class Program
    {
        static void Main()
        {
            var cache = new DistributedCache<string, string>();
            cache.AddNode("Node1");
            cache.AddNode("Node2");
            cache.Set("user:1", "Alice", TimeSpan.FromMinutes(5));
            Console.WriteLine(cache.Get("user:1"));
            Console.WriteLine("Node for key: " + cache.GetNodeForKey("user:1"));
        }
    }
}
