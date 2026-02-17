namespace ValidationDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new Models.Context.AppDbContext())
            {
                var student = new Models.Student
                {
                    Name = "John Doe",
                    Email = "vivaswan2002@gmail.com"
                };
                context.Students.Add(student);
                context.SaveChanges();
                Console.WriteLine("Student added successfully!");
            }
        }
    }
}
