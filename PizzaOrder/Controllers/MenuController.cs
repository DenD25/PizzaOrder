using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaOrder.Models;

namespace PizzaOrder.Controllers
{
    public class MenuController : Controller
    {
        ApplicationContext db;

        public MenuController(ApplicationContext context)
        {
            db = context;
        }

        public IActionResult Menu()
        {
            List<Pizza> pizzas = db
                .Pizzas
                .Include(x => x.PizzaComponents)
                .ToList();

            return View(pizzas);
        }
    }
}
