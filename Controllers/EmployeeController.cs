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

        public async Task<IActionResult> Detail(int id)
        { 
            Employee employee = await _employeeRepository.GetByIdAsync(id);
            return View(employee);
        }
    }
}
