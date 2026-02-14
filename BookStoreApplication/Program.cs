using System;

namespace BookStoreApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO:
            // 1. Read initial input
            // Format: BookID Title Price Stock
            string[] data = Console.ReadLine().Split(' ');
        
            Book book = new Book();
            book.Id=data[0];
            book.Title=data[1];
            book.Price= Convert.ToInt32(data[2]);
            book.Stock= Convert.ToInt32(data[3]);


            

            BookUtility utility = new BookUtility(book);

            while (true)
            {
                int choice = int.Parse(Console.ReadLine() ?? "0");

                switch (choice)
                {
                    case 1:
                        utility.GetBookDetails();
                        break;

                    case 2:
                        int newPrice = int.Parse(Console.ReadLine() ?? "0");
                        utility.UpdateBookPrice(newPrice);
                        break;

                    case 3:
                        int newStock = int.Parse(Console.ReadLine() ?? "0");
                        utility.UpdateBookStock(newStock);
                        break;

                    case 4:
                        Console.WriteLine("Thank You");
                        return;

                    default:
                        break;
                }
            }
        }
    }
}
