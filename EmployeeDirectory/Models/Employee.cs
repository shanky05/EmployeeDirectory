namespace EmployeeDirectory.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? ManagerID { get; set; }
        public Employee? Manager { get; set; }
    }
}
