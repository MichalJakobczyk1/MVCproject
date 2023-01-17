using Microsoft.AspNetCore.Mvc;
using MVCproject.Models;

namespace MVCproject.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
