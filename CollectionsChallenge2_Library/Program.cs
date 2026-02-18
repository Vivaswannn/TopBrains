using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionsChallenge2_Library
{
    public class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public bool IsAvailable { get; set; } = true;
    }

    public class Catalog<T> where T : Book
    {
        private readonly List<T> _items = new List<T>();
        private readonly HashSet<string> _isbnSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private readonly SortedDictionary<string, List<T>> _genreIndex = new SortedDictionary<string, List<T>>(StringComparer.OrdinalIgnoreCase);

        public bool AddItem(T item)
        {
            if (item == null || string.IsNullOrEmpty(item.ISBN)) return false;
            if (_isbnSet.Contains(item.ISBN)) return false;
            _isbnSet.Add(item.ISBN);
            _items.Add(item);
            var genre = item.Genre ?? "";
            if (!_genreIndex.ContainsKey(genre))
                _genreIndex[genre] = new List<T>();
            _genreIndex[genre].Add(item);
            return true;
        }

        public List<T> this[string genre]
        {
            get
            {
                if (string.IsNullOrEmpty(genre)) return new List<T>();
                return _genreIndex.TryGetValue(genre, out var list) ? new List<T>(list) : new List<T>();
            }
        }

        public IEnumerable<T> FindBooks(Func<T, bool> predicate)
        {
            return _items.Where(predicate);
        }
    }

    class Program
    {
        static void Main()
        {
            var library = new Catalog<Book>();
            var book1 = new Book
            {
                ISBN = "978-3-16-148410-0",
                Title = "C# Programming",
                Author = "John Sharp",
                Genre = "Programming"
            };
            library.AddItem(book1);

            var programmingBooks = library["Programming"];
            Console.WriteLine(programmingBooks.Count);

            var johnsBooks = library.FindBooks(b => b.Author != null && b.Author.Contains("John"));
            Console.WriteLine(johnsBooks.Count());
        }
    }
}
