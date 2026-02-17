using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace ConnectEnvDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {

            DisconnectEnvDemo disconnectedEnv = new DisconnectEnvDemo();
            disconnectedEnv.GetAllStudents();
            Console.WriteLine("\n\n\n");



            Console.WriteLine("enter student id: ");
            int id = Convert.ToInt32(Console.ReadLine());

            disconnectedEnv.GetStudentById(id);





            //ConnectedEnv connectedEnv = new ConnectedEnv();
            //connectedEnv.GetAllStudent();
            //Console.WriteLine("\n\n\n");
            ////Console.WriteLine("Enter Student Id to search:");
            ////int Id= Convert.ToInt32(Console.ReadLine());
            ////connectedEnv.GetStudentById(Id);

            //Console.WriteLine("enter student details to add:");
            //Console.WriteLine("enter student id");
            //int studid = Convert.ToInt32(Console.ReadLine());
            //connectedEnv.UpdateStudent(studid, );
            //Console.WriteLine("\n\n\n");

            //Console.WriteLine("enter Course name to search:");
            //string course = Console.ReadLine();
            //connectedEnv.GetStudentByCourse(course);


            //Console.ReadLine();
        }
    }
}
