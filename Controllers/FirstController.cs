using Microsoft.AspNetCore.Mvc;
using BookStoreMVC.Models;

namespace BookStoreMVC.Controllers
{
    public class FirstController : Controller
    {
        public String Index()
        {
            return "Hello World";
        }

        public IActionResult Hello()
        {
            ViewBag.Message = "Hello World";
            return View();
        }

        public IActionResult Info()
        {
            Employee employee = Repository.AllEmployees.Where(e => e.Name == "John").FirstOrDefault();
            return View(employee);
        }
    }
}
