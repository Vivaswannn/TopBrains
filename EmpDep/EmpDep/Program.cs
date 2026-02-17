using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpDep
{
internal class Program
    {
        // 1. Connection String: The address of your database.
        // "Data Source=.\SQLEXPRESS" -> Points to the SQL Server Express instance on this machine.
        // "Initial Catalog=CompanyDB" -> The specific database we created.
        // "Integrated Security=True"  -> Uses your Windows login (no password needed).
        static string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=CompanyDB;Integrated Security=True";

        static void Main(string[] args)
        {
            while (true)
            {
                // Simple Console Menu
                Console.Clear();
                Console.WriteLine("====== COMPANY MANAGEMENT ======");
                Console.WriteLine("1. Add Department");
                Console.WriteLine("2. Add Employee");
                Console.WriteLine("3. View Departments");
                Console.WriteLine("4. View Employees");
                Console.WriteLine("5. View Employee with Department");
                Console.WriteLine("6. Update Employee Salary");
                Console.WriteLine("7. Delete Employee");
                Console.WriteLine("8. Delete Department");
                Console.WriteLine("0. Exit");
                Console.WriteLine("================================");
                Console.Write("Enter choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": InsertDepartment(); break;
                    case "2": InsertEmployee(); break;
                    case "3": ViewDepartments(); break;
                    case "4": ViewEmployees(); break;
                    case "5": ViewEmployeeDepartment(); break;
                    case "6": UpdateSalary(); break;
                    case "7": DeleteEmployee(); break;
                    case "8": DeleteDepartment(); break;
                    case "0": return;
                    default:
                        Console.WriteLine("Invalid choice! Press Enter to try again.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        // 1. Insert Department (INSERT Example)
        static void InsertDepartment()
        {
            Console.WriteLine("\n--- Add Department ---");
            Console.Write("Enter Department Id: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Enter Department Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Location: ");
            string location = Console.ReadLine();

            // STEP 1: Create the connection (The "Phone Line")
            // 'using' ensures the connection is closed automatically when done.
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // STEP 2: Create the Command (The "Order")
                // We use @parameters to prevent SQL Injection (security).
                string query = "INSERT INTO Departments VALUES (@id, @name, @loc)";
                SqlCommand cmd = new SqlCommand(query, con);
                
                // STEP 3: Add Parameters (Fill in the blanks)
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@loc", location);

                try
                {
                    // STEP 4: Open Connection (Dial the number)
                    con.Open();
                    
                    // STEP 5: Execute (Send the order)
                    // ExecuteNonQuery is used for INSERT, UPDATE, DELETE.
                    // It returns the number of rows affected.
                    int rows = cmd.ExecuteNonQuery();
                    Console.WriteLine($"{rows} Department inserted successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            } // Connection closes here automatically
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        // 2. Insert Employee (INSERT with Validation)
        static void InsertEmployee()
        {
            Console.WriteLine("\n--- Add Employee ---");
            Console.Write("Enter Employee Id: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Salary: ");
            decimal salary = decimal.Parse(Console.ReadLine());
            Console.Write("Enter Department Id: ");
            int deptId = int.Parse(Console.ReadLine());

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // VALIDATION: Check if Department exists first
                string checkQuery = "SELECT COUNT(*) FROM Departments WHERE DepartmentId=@did";
                SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@did", deptId);

                // ExecuteScalar returns a single value (the count)
                int count = (int)checkCmd.ExecuteScalar();

                if (count == 0)
                {
                    Console.WriteLine("Invalid Department Id! Cannot add employee.");
                }
                else
                {
                    // If validation passes, proceed with Insert
                    string query = "INSERT INTO Employees VALUES (@id, @name, @sal, @did)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@sal", salary);
                    cmd.Parameters.AddWithValue("@did", deptId);

                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        Console.WriteLine($"{rows} Employee inserted successfully!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        // 3. View All Departments (SELECT Example)
        static void ViewDepartments()
        {
            Console.WriteLine("\n--- All Departments ---");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Departments";
                SqlCommand cmd = new SqlCommand(query, con);
                
                try
                {
                    con.Open();
                    
                    // ExecuteReader is used for SELECT. It returns a stream of data.
                    SqlDataReader reader = cmd.ExecuteReader();

                    Console.WriteLine("ID\tName\tLocation");
                    Console.WriteLine("-------------------------");
                    
                    // Loop through the rows one by one
                    while (reader.Read())
                    {
                        // Access columns by name: reader["ColumnName"]
                        Console.WriteLine($"{reader["DepartmentId"]}\t{reader["DepartmentName"]}\t{reader["Location"]}");
                    }
                    reader.Close(); // Always close the reader when done!
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        // 4. View All Employees
        static void ViewEmployees()
        {
            Console.WriteLine("\n--- All Employees ---");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Employees";
                SqlCommand cmd = new SqlCommand(query, con);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    Console.WriteLine("ID\tName\tSalary\tDeptID");
                    Console.WriteLine("--------------------------------");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["EmployeeId"]}\t{reader["EmployeeName"]}\t{reader["Salary"]}\t{reader["DepartmentId"]}");
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        // 5. View Employee with Department Name (JOIN Example)
        static void ViewEmployeeDepartment()
        {
            Console.WriteLine("\n--- Employee Details with Department ---");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // JOIN Query: Combines data from two tables
                string query = @"SELECT e.EmployeeId, e.EmployeeName, e.Salary, d.DepartmentName 
                                 FROM Employees e 
                                 JOIN Departments d ON e.DepartmentId = d.DepartmentId";
                SqlCommand cmd = new SqlCommand(query, con);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    Console.WriteLine("ID\tName\tSalary\tDept Name");
                    Console.WriteLine("------------------------------------");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["EmployeeId"]}\t{reader["EmployeeName"]}\t{reader["Salary"]}\t{reader["DepartmentName"]}");
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        // 6. Update Employee Salary (UPDATE Example)
        static void UpdateSalary()
        {
            Console.WriteLine("\n--- Update Salary ---");
            Console.Write("Enter Employee Id: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Enter New Salary: ");
            decimal salary = decimal.Parse(Console.ReadLine());

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Employees SET Salary=@sal WHERE EmployeeId=@id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@sal", salary);
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    con.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                        Console.WriteLine("Salary updated successfully!");
                    else
                        Console.WriteLine("Employee not found.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        // 7. Delete Employee (DELETE Example)
        static void DeleteEmployee()
        {
            Console.WriteLine("\n--- Delete Employee ---");
            Console.Write("Enter Employee Id: ");
            int id = int.Parse(Console.ReadLine());

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Employees WHERE EmployeeId=@id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    con.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                        Console.WriteLine("Employee deleted successfully!");
                    else
                        Console.WriteLine("Employee not found.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        // 8. Delete Department (DELETE with Validation)
        static void DeleteDepartment()
        {
            Console.WriteLine("\n--- Delete Department ---");
            Console.Write("Enter Department Id: ");
            int id = int.Parse(Console.ReadLine());

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Validation: Check if Employees exist in this department
                string checkQuery = "SELECT COUNT(*) FROM Employees WHERE DepartmentId=@id";
                SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                checkCmd.Parameters.AddWithValue("@id", id);

                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    Console.WriteLine("Cannot delete department – employees assigned!");
                }
                else
                {
                    string query = "DELETE FROM Departments WHERE DepartmentId=@id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", id);

                    try
                    {
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                            Console.WriteLine("Department deleted successfully!");
                        else
                            Console.WriteLine("Department not found.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
