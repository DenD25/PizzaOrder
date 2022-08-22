using Microsoft.AspNetCore.Mvc;
using PizzaOrder.Models;
using System.Diagnostics;

namespace PizzaOrder.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}