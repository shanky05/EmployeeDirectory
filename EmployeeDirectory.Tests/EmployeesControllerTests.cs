using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using EmployeeDirectory.Controllers;
using EmployeeDirectory.Models;
using EmployeeDirectory.Repository;

namespace EmployeeDirectory.Tests
{
    public class EmployeesControllerTests
    {
        private readonly EmployeesController _controller;
        private readonly Mock<IEmployeeRepository> _mockRepository;

        public EmployeesControllerTests()
        {
            _mockRepository = new Mock<IEmployeeRepository>();
            _controller = new EmployeesController(_mockRepository.Object);
        }

        [Fact]
        public void Index_Returns_ViewResult_With_Employees()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee { EmployeeID = 1, FirstName = "John", LastName = "Doe" },
                new Employee { EmployeeID = 2, FirstName = "Jane", LastName = "Smith" }
            };
            _mockRepository.Setup(repo => repo.GetEmployees()).Returns(employees);

            // Act
            var result = _controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = result.Model as List<Employee>;
            Assert.NotNull(model);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public void Create_ValidModelState_Redirects_To_ListView()
        {
            // Arrange
            var employee = new Employee { EmployeeID = 1, FirstName = "John", LastName = "Doe" };
            _controller.ModelState.Clear();

            // Act
            var result = _controller.Create(employee) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("ListView", result.ActionName);
            _mockRepository.Verify(repo => repo.CreateEmployee(employee), Times.Once);
        }

        [Fact]
        public void Create_InvalidModelState_Returns_ListView_With_Employees()
        {
            // Arrange
            var employee = new Employee { EmployeeID = 1, FirstName = "John", LastName = "Doe" };
            _controller.ModelState.AddModelError("Name", "Required");
            var employees = new List<Employee> { employee };
            _mockRepository.Setup(repo => repo.GetEmployees()).Returns(employees);

            // Act
            var result = _controller.Create(employee) as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = result.Model as List<Employee>;
            Assert.NotNull(model);
            Assert.Single(model);
            _mockRepository.Verify(repo => repo.CreateEmployee(It.IsAny<Employee>()), Times.Never);
        }

        [Fact]
        public void Delete_Deletes_Employee_And_Redirects_To_ListView()
        {
            // Arrange
            int employeeId = 1;

            // Act
            var result = _controller.Delete(employeeId) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("ListView", result.ActionName);
            _mockRepository.Verify(repo => repo.DeleteEmployee(employeeId), Times.Once);
        }
    }
}
