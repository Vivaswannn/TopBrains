using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectEnvDemo
{
    internal class DisconnectEnvDemo
    {
        string conString = "data source=.; database=UniversityDB; integrated security=SSPI";

        public void GetAllStudents()
        {
            SqlConnection sqlConnection = new SqlConnection(conString);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Student", sqlConnection);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet, "Student");
            foreach (DataRow dataRow in dataSet.Tables["Student"].Rows)
            {
                Console.WriteLine($"Student id is {dataRow["StudentID"]} Name is {dataRow["Name"]} Course is {dataRow["Course"]} Marks ars{dataRow["Marks"]} ");
            }
        }
        public void GetStudentById(int id) { 
            SqlConnection sqlConnection = new SqlConnection(conString);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Student", sqlConnection);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet, "Student");
            bool isStudentFound = false;
            for(int i=0; i<dataSet.Tables["Student"].Rows.Count; i++)
            {
                if (Convert.ToInt32(dataSet.Tables["Student"].Rows[i]["StudentID"]) == id)
                {
                    Console.WriteLine($"Student id is {dataSet.Tables["Student"].Rows[i][0]} Name is {dataSet.Tables["Student"].Rows[i][1]} Course is {dataSet.Tables["Student"].Rows[i][2]} Marks ars{dataSet.Tables["Student"].Rows[i][3]} ");

                    isStudentFound = true;
                    break;
                }
            }
            if(!isStudentFound)
            {
                Console.WriteLine("Student with id {id} not found");
            }
        }

        public void AddNewStudent() {
            SqlConnection sqlConnection = new SqlConnection(conString);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("select * from Student", sqlConnection);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet, "Student");
            DataRow dataRow = dataSet.Tables["Student"].NewRow();
            Console.WriteLine("enter student details");
            Console.WriteLine("enter student id");
            int studentid = Convert.ToInt32(Console.ReadLine());
            dataRow[0] = studentid;
            Console.WriteLine("enter student name");


        }

       
    }
}
