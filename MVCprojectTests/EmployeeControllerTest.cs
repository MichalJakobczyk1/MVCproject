using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MVCproject.Data.Enum;
using MVCproject.Controllers;
using MVCproject.Interfaces;
using MVCproject.Models;
using MVCproject.ViewModels;
using Xunit;

namespace EmployeeControllerUnitTest
{
    public class EmployeeControllerTest
    {
        private readonly EmployeeController _controller;
        private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
        private readonly Employee _employee;

        public EmployeeControllerTest()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _controller = new EmployeeController(_employeeRepositoryMock.Object, null);
            _employee = new Employee { Id = 1, Name = "John", Surname = "Doe" };
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfEmployees()
        {
            _employeeRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(new[] { _employee });
            var result = await _controller.Index();
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Employee>>(viewResult.Model);
            Assert.Equal(1, model.Count());
        }

        [Fact]
        public async Task Create_Post_AddsEmployee_RedirectsToIndex()
        {
            var createEmployeeViewModel = new CreateEmployeeViewModel
            {
                Name = "John",
                Surname = "Doe",
                ContactNumber = "1234567890",
                Email = "johndoe@gmail.com",
                Address = new Address { City = "Kraków", Street = "Krakowska" },
                Info = new Info { Role = Role.Bartender, Level = Level.Intermediate }
            };
            var result = await _controller.Create(createEmployeeViewModel);

            _employeeRepositoryMock.Verify(x => x.Add(It.IsAny<Employee>()), Times.Once);
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Delete_EmployeeNotFound_ReturnsErrorView()
        {
            _employeeRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Employee)null);

            var result = await _controller.Delete(1);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Error", viewResult.ViewName);
        }
    }
}