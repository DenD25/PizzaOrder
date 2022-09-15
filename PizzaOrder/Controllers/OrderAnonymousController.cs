using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaOrder.Models;
using PizzaOrder.ViewModel;

namespace PizzaOrder.Controllers
{
    public class OrderAnonymousController : Controller
    {
        ApplicationContext db;

        public OrderAnonymousController(ApplicationContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
