using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCproject.Interfaces;
using MVCproject.Models;
using MVCproject.ViewModels;
using System.Net;

namespace MVCproject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Employee> employees = await _employeeRepository.GetAll();
            return View(employees);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            _employeeRepository.Add(employee);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var employeeDetails = await _employeeRepository.GetByIdAsync(id);
            if (employeeDetails == null) return View("Error");
            return View(employeeDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employeeDetails = await _employeeRepository.GetByIdAsync(id);
            if (employeeDetails == null) return View("Error");

            _employeeRepository.Delete(employeeDetails);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null) return View("Error");
            var employeeVM = new EditEmployeeViewModel
            {
                Id = id,
                Name = employee.Name,
                Surname = employee.Surname,
                ContactNumber = employee.ContactNumber,
                Email = employee.Email,
            };
            return View(employeeVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditEmployeeViewModel editEmployeeViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit employee");
                return View("Edit", editEmployeeViewModel);
            }

            var employee = await _employeeRepository.GetByIdAsyncNoTracking(id);

            if (employee != null)
            {
                var emp = new Employee
                {
                    Id = id,
                    Name = editEmployeeViewModel.Name,
                    Surname = editEmployeeViewModel.Surname,
                    ContactNumber = editEmployeeViewModel.ContactNumber,
                    Email = editEmployeeViewModel.Email,
                };

                _employeeRepository.Update(emp);
                return RedirectToAction("Index");
            }
            else
            {
                return View(editEmployeeViewModel);
            }
        }

        public async Task<IActionResult> Detail(int id)
        { 
            Employee employee = await _employeeRepository.GetByIdAsync(id);
            return View(employee);
        }
    }
}
