using EmployeeDirectory.Models;

namespace EmployeeDirectory.Repository
{
    public interface IEmployeeRepository
    {
        List<Employee> GetEmployees();
        void CreateEmployee(Employee employee);
        void DeleteEmployee(int id);
    }
}
