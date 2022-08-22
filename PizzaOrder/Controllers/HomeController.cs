using Microsoft.AspNetCore.Mvc;
using PizzaOrder.Models;
using System.Diagnostics;

namespace PizzaOrder.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}