using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaOrder.Models;

namespace PizzaOrder.Areas.User.Controllers
{
    public class MenuUserController : Controller
    {
        ApplicationContext db;

        public MenuUserController(ApplicationContext context)
        {
            db = context;
        }

        public IActionResult MenuUser()
        {
            var pizza = db
                .Pizzas
                .Include(x => x.PizzaComponents)
                .ToList();

            return View(pizza);
        }
    }
}
