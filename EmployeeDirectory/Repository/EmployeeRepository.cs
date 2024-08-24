using EmployeeDirectory.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace EmployeeDirectory.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Employee> GetEmployees()
        {
            var employees = new List<Employee>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT e.EmployeeID ,e.FirstName ,e.LastName, " +
                    "m.FirstName , m.LastName , " +
                    "m.EmployeeID FROM Employees e LEFT JOIN Employees m ON e.ManagerID = m.EmployeeID;", connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    employees.Add(new Employee
                    {
                        EmployeeID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Manager = new Employee { FirstName = reader.IsDBNull(3) ? null : reader.GetString(3), LastName = reader.IsDBNull(4) ? null : reader.GetString(4) },
                        ManagerID = reader.IsDBNull(5) ? (int?)null : reader.GetInt32(5)
                    });
                }
            }

            return employees;
        }

        public void CreateEmployee(Employee employee)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO Employees (FirstName, LastName, ManagerID) VALUES (@FirstName, @LastName, @ManagerID)", connection);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@ManagerID", employee.ManagerID != null ? employee.ManagerID : DBNull.Value);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteEmployee(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Employees WHERE EmployeeID = @EmployeeID", connection);
                command.Parameters.AddWithValue("@EmployeeID", id);
                command.ExecuteNonQuery();
            }
        }
    }
}
