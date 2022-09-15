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

        public IActionResult Index(int? id)
        {
            List<Pizza> pizzas = db.Pizzas.ToList();
            List<PizzaComponent> pizzaComponents = db.PizzaComponents.ToList();
            Order order = db
                .OrderUsers
                .FirstOrDefault(x => x.Id == id);

            OrderUserViewModel ouvm = new OrderUserViewModel { OrderUser = order, PizzaComponents = pizzaComponents, Pizzas = pizzas };

            return View(ouvm);
        }

        [HttpPost]
        public async Task<IActionResult> Index( int count, int pizzaId)
        {
            Order order = new Order();

            order.Name = "Name";
            order.CreateTime = DateTime.Now;

            Pizza orderPizza = db
                    .Pizzas
                    .FirstOrDefault(x => x.Id == pizzaId);

            List<Pizza> pizzaList = new List<Pizza>();

            orderPizza.Count = count;

            pizzaList.Add(orderPizza);

            order.Pizzas = pizzaList;

            db.OrderUsers.Add(order);
            await db.SaveChangesAsync();
            
            return RedirectToAction("AddingPizzas", new { id = order.Id });           
        }

        public IActionResult AddingPizzasAnonymous(int id)
        {
            Order order = db
                .OrderUsers
                .Include(x => x.Pizzas)
                .FirstOrDefault(x => x.Id == id);

            return View(order);
        }
    }
}
