namespace EmployeeDirectory.Models
{
    public class EmployeeTreeViewModel
    {
        public Employee Employee { get; set; }
        public List<EmployeeTreeViewModel> Subordinates { get; set; } = new List<EmployeeTreeViewModel>();
    }
}
