using Devart.Data.MySql;
using System;
namespace ConnectedEnvDemo
{
    internal class Class1
    {

        MySqlConnection connection;
        MySqlCommand command;
        MySqlDataReader reader;

        public Class1()
        {

        }
        public void GetAllStudent()
        {
            connection = new MySqlConnection("Server=localhost;Port=3306;Database=testdb;User Id=root;Password=root;SslMode=None;\"\r\n         providerName=\"MySql.Data.MySqlClient\"");
            connection.Open();
            command = new MySqlCommand("select * from Student", connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = Convert.ToInt32(reader["StudentId"]);
                string name = reader["StudentName"].ToString();
                string course = reader["Course"].ToString();
                int marks = Convert.ToInt32(reader["Marks"]);

                Console.WriteLine($"ID: {id}, Name: {name}, Course: {course}, Marks: {marks}");
            }
            connection.Close();
        }

    }
}
