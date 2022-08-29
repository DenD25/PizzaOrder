using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaOrder.Models;
using PizzaOrder.ViewModel;

namespace PizzaOrder.Areas.User.Controllers
{
    public class OrderUserController : Controller
    {
        ApplicationContext db;

        public OrderUserController(ApplicationContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            List<Pizza> pizzas = db.Pizzas.ToList();
            List<PizzaComponent> pizzaComponents = db.PizzaComponents.ToList();
            OrderUser order = new OrderUser();

            OrderUserViewModel ouvm = new OrderUserViewModel { OrderUser = order, PizzaComponents = pizzaComponents, Pizzas = pizzas };

            return View(ouvm);
        }
    }
}
