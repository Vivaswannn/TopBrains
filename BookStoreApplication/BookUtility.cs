using System;

namespace BookStoreApplication
{
    public class BookUtility
    {
        private Book _book;

        public BookUtility(Book book)
        {
            // TODO: Assign book object
            _book=book;
        }

        public void GetBookDetails()
        {
            // TODO:
            // Print format:
            // Details: <BookId> <Title> <Price> <Stock>
            Console.WriteLine($"Details: {_book.Id} {_book.Title} {_book.Price} {_book.Stock}");

        }

        public void UpdateBookPrice(int newPrice)
        {
            _book.Price = newPrice;
            Console.WriteLine($"Updated Price: {newPrice}");
        }

        public void UpdateBookStock(int newStock)
        {
            _book.Stock = newStock;
            Console.WriteLine($"Updated Stock: {newStock}");
        }
    }
}
