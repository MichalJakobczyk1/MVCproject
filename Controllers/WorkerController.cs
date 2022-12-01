using Microsoft.AspNetCore.Mvc;
using MVCproject.Models;

namespace MVCproject.Controllers
{
    public class WorkerController : Controller
    {
        public List<Worker> workers = new List<Worker>() 
        { 
            new Worker() { id = 1, hiredOn = new DateTime(2020,01,13),name = "Michał",surname="Nowak",contactNumber="512456890",email="nowak@gmail.com"}
        };
        [HttpPost]
        public IActionResult Add([FromForm] Worker worker)
        {
            if (ModelState.IsValid)
            {
                int counter = workers.Count();
                worker.id = counter++;
                workers.Add(worker);
                return View("Index", workers);
            }

            return View();
        }
        public IActionResult Edit([FromRoute] int id)
        {
            var worker = workers.FirstOrDefault(w => w.id == id);
            return View(worker);
        }
        [HttpPost]
        public IActionResult Edit([FromForm] Worker worker, [FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                var index = workers.FindIndex(w => w.id == id);
                workers[index] = worker;
                return View("Index", workers);
            }
            return View();
        }

        public IActionResult Delete([FromRoute] int id)
        {
            Worker w = workers.FirstOrDefault(w => w.id == id);
            if (w != null)
            {
                workers.Remove(w);
            }
            return View("Index", workers);
        }
        public IActionResult Index()
        {
            return View(workers);
        }
    }
}
