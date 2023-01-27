using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCproject.Data;
using MVCproject.Interfaces;
using MVCproject.Models;
using MVCproject.Repositories;
using MVCproject.Services;
using MVCproject.ViewModels;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MVCproject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly AppDbContext _context;
        public EmployeeController(IEmployeeRepository employeeRepository, AppDbContext context)
        {
            _employeeRepository = employeeRepository;
            _context = context;
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeViewModel createEmployeeViewModel)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    Name = createEmployeeViewModel.Name,
                    Surname = createEmployeeViewModel.Surname,
                    ContactNumber = createEmployeeViewModel.ContactNumber,
                    Email = createEmployeeViewModel.Email,
                    Address = new Address
                    {
                        City = createEmployeeViewModel.Address.City,
                        Street = createEmployeeViewModel.Address.Street,
                    },
                    Info = new Info
                    { 
                        Role = createEmployeeViewModel.Info.Role,
                        Level = createEmployeeViewModel.Info.Level
                    }
                };
                _employeeRepository.Add(employee);
                return RedirectToAction("Index");
            }
            return View(createEmployeeViewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var employeeDetails = await _employeeRepository.GetByIdAsync(id);
            if (employeeDetails == null) return View("Error");
            return View(employeeDetails);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employeeDetails = await _employeeRepository.GetByIdAsync(id);
            if (employeeDetails == null) return View("Error");

            _employeeRepository.Delete(employeeDetails);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null) return View("Error");
            var employeeViewModel = new EditEmployeeViewModel
            {
                Id = id,
                Name = employee.Name,
                Surname = employee.Surname,
                ContactNumber = employee.ContactNumber,
                Email = employee.Email,
                AddressId = employee.AddressId,
                Address = employee.Address,
                InfoId = employee.InfoId,
                Info = employee.Info
            };
            return View(employeeViewModel);
        }

        [Authorize(Roles = "Admin")]
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
                    AddressId = editEmployeeViewModel.AddressId,
                    Address = editEmployeeViewModel.Address,
                    InfoId = editEmployeeViewModel.InfoId,
                    Info = editEmployeeViewModel.Info
                };

                _employeeRepository.Update(emp);
                return RedirectToAction("Index");
            }
            else
            {
                return View(editEmployeeViewModel);
            }
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Detail(int id)
        { 
            Employee employee = await _employeeRepository.GetByIdAsync(id);
            return View(employee);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Search(string name, string surname)
        {
            var employee = from m in _context.Employees
                           select m;

            if (!string.IsNullOrEmpty(name))
            {
                employee = employee.Where(x => x.Name!.Contains(name));
            }

            if (!string.IsNullOrEmpty(surname))
            {
                employee = employee.Where(x => x.Surname!.Contains(surname));
            }

            return View(employee.ToList());
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OrderByName()
        {
            return View(_context.Employees.OrderByDescending(e => e.Name).ToList());
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OrderBySurname()
        {
            return View(_context.Employees.OrderByDescending(e => e.Surname).ToList());
        }
    }
}
