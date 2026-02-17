using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConnectEnvDemo
{
    internal class ConnectedEnv
    {
        string ConString = "";
        SqlDataReader dataReader;
        public void GetAllStudent()
        {
            
            string sqlCmd = "Select * from students";
            SqlConnection sqlConnection = new SqlConnection(ConString);
            sqlConnection.Open();
            Console.WriteLine("Connection Opened");
            //hard coding sql dml query in command object leads to sql injection attack
            SqlCommand sqlCommand = new SqlCommand(sqlCmd, sqlConnection);
            sqlCommand.ExecuteReader();
            dataReader = sqlCommand.ExecuteReader();

            while (dataReader.Read())
            {
                int StudentID = Convert.ToInt32(dataReader["StudentID"]);
                string StudentName = dataReader["Name"].ToString();
                string Course = dataReader["Course"].ToString();
                int Marks = Convert.ToInt32(dataReader["Marks"]);
                Console.WriteLine($"ID: {StudentID} Name: {StudentName} Course: {Course} Marks: {Marks}");
            }
        }

        public void GetStudentByCourse(string course)
        {
            string sqlCommand = "select * from Students where Course= 'course'";
            SqlConnection sqlConnection = new SqlConnection(ConString);
            sqlConnection.Open();
            Console.WriteLine("Connection Opened");
            SqlCommand cmd = new SqlCommand(sqlCommand, sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader[0]} Name is {reader[1].ToString()} " +
                    $"Course is {reader[2].ToString()} Marks are {reader[3].ToString()}");
            }
            sqlConnection.Close();
        }

        public void GetStudentById(int id)
        {
            string sqlCommand = "select * from Students where StudentID= 50";
            SqlConnection sqlConnection = new SqlConnection(ConString);
            sqlConnection.Open();
            Console.WriteLine("Connection Opened");
            SqlCommand cmd = new SqlCommand(sqlCommand, sqlConnection);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader[0]} Name is {reader[1].ToString()} "+
                    $"Course is {reader[2].ToString()} Marks are {reader[3].ToString()}");

            }
            sqlConnection.Close();
        }

        public void GetStudentByName(string Course)
        {
            
        }
        public void AddNewStudent(int id, string Name, string Course, int Marks)
        {
            //string sqlCmd = "Insert into Students(700, 'Ajay', 'Java', 75)";
            string sqlCmd=$"Insert into Students({id}, '{Name}', '{Course}', {Marks})";
            SqlConnection sqlConnection = new SqlConnection(ConString);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand(sqlCmd, sqlConnection);
            int count = cmd.ExecuteNonQuery();

            sqlConnection.Close();

            Console.WriteLine("Students registered successfully");
            Console.WriteLine($"Number of rows affected: {count}");


        }
        public void UpdateStudent(int id, string Name, string Course, int Marks) { 
        string sqlCmd= "Update Students set Name='Ajay', Course='Java', Marks=75 where StudentID=700";
            SqlConnection sqlConnection = new SqlConnection(ConString);
            sqlConnection.Open();
            SqlCommand cmd = new SqlCommand(sqlCmd, sqlConnection);
            int count = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            Console.WriteLine("Students updated successfully");
            Console.WriteLine($"Number of rows affected: {count}");
        }
        public void DeleteStudent(int id) { }
    }
}
