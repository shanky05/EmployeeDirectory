using EmployeeDirectory.Models;
using EmployeeDirectory.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDirectory.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _repository;

        public EmployeesController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            var employees = _repository.GetEmployees();
            return View("ListView", employees);
        }

        public IActionResult TreeView()
        {
            var employees = _repository.GetEmployees();
            return View(employees);
        }

        public IActionResult ListView()
        {
            var employees = _repository.GetEmployees();
            return View(employees);
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _repository.CreateEmployee(employee);
                return RedirectToAction("ListView");
            }
            return View("ListView", _repository.GetEmployees());
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _repository.DeleteEmployee(id);
            return RedirectToAction("ListView");
        }
    }
}
