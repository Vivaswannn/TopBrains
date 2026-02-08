using System;
using System.Collections.Generic;
using System.IO;

namespace EnterpriseLogSystem
{
    public class LogEntry
    {
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }

        public LogEntry(string message)
        {
            Message = message;
            CreatedAt = DateTime.Now;
        }
    }

    public class LogCache
    {
        private List<LogEntry> _cache = new List<LogEntry>();

        public void Add(LogEntry entry) => _cache.Add(entry);
        public void Clear() => _cache.Clear();
    }

 
    public class FileLogger : IDisposable
    {
        private StreamWriter? _writer;
        private bool disposed = false;

        public FileLogger(string filePath)
        {
            _writer = new StreamWriter(filePath, append: true);
            Console.WriteLine("File resource acquired.");
        }

        public void WriteLog(string message)
        {
            if (disposed || _writer == null)
                throw new ObjectDisposedException(nameof(FileLogger));

            _writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
            _writer.Flush();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~FileLogger() => Dispose(false);

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Console.WriteLine("Managed resources released.");
                    if (_writer != null)
                    {
                        _writer.Dispose();
                        _writer = null;
                    }
                }
                Console.WriteLine("Unmanaged resources released.");
                disposed = true;
            }
        }
    }
    public class LogProcessor
    {
        public void ProcessLogs()
        {
            long initialMemory = GC.GetTotalMemory(false);
            Console.WriteLine($"Initial Memory: {initialMemory:N0} bytes");

            var cache = new LogCache();
            for (int i = 0; i < 10000; i++)
                cache.Add(new LogEntry($"Log entry {i}"));

            long memoryAfterCreation = GC.GetTotalMemory(false);
            Console.WriteLine($"Memory After Log Creation: {memoryAfterCreation:N0} bytes");

            var weakCacheRef = new WeakReference(cache);
            cache.Clear();
            cache = null;

            Console.WriteLine($"Is Cache Alive (WeakReference) Before GC: {weakCacheRef.IsAlive}");

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            long memoryAfterGC = GC.GetTotalMemory(false);
            Console.WriteLine($"Memory After GC: {memoryAfterGC:N0} bytes");
            Console.WriteLine($"Is Cache Alive (WeakReference) After GC: {weakCacheRef.IsAlive}");
        }
    }
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== Enterprise Log Processing System ===");
            Console.WriteLine();

            using (var logger = new FileLogger("app.log"))
                logger.WriteLog("Started");
            Console.WriteLine("FileLogger disposed.");
            Console.WriteLine();

            new LogProcessor().ProcessLogs();
            Console.WriteLine();

            Console.WriteLine("=== Generational GC Demonstration ===");
            var newObj = new object();
            Console.WriteLine($"Generation of new object: {GC.GetGeneration(newObj)}");

            var longLivedCache = new LogCache();
            var sampleEntry = new LogEntry("Sample entry");
            longLivedCache.Add(sampleEntry);
            
            GC.Collect(0, GCCollectionMode.Forced);
            Console.WriteLine($"Generation of long-lived cache (after Gen 0 collection): {GC.GetGeneration(longLivedCache)}");
            Console.WriteLine($"Generation of cache entry (after Gen 0 collection): {GC.GetGeneration(sampleEntry)}");

            GC.Collect();
            GC.Collect();
            Console.WriteLine($"Generation of application-wide cache (after full collections): {GC.GetGeneration(longLivedCache)}");

            Console.WriteLine();
            Console.WriteLine("=== Creating Thousands of Short-Lived Objects ===");
            long memBefore = GC.GetTotalMemory(false);
            
            for (int i = 0; i < 50000; i++)
                _ = new LogEntry($"System started - iteration {i}");
            
            long memAfter = GC.GetTotalMemory(false);
            Console.WriteLine($"Memory before: {memBefore:N0} bytes");
            Console.WriteLine($"Memory after creating 50,000 objects: {memAfter:N0} bytes");
            
            GC.Collect(0, GCCollectionMode.Forced);
            Console.WriteLine($"Memory after Gen 0 GC: {GC.GetTotalMemory(false):N0} bytes");

            Console.WriteLine();
            Console.WriteLine("Application execution completed.");
        }
    }
}

