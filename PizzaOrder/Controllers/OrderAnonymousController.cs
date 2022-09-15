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
            
            return RedirectToAction("AddingPizzasAnonymous", new { id = order.Id });           
        }

        public IActionResult AddingPizzasAnonymous(int id)
        {
            Order order = db
                .OrderUsers
                .Include(x => x.Pizzas)
                .FirstOrDefault(x => x.Id == id);

            return View(order);
        }

        public IActionResult AddingDataAnonymous(int id)
        {
            Order orderAnonymous = db
                .OrderUsers
                .FirstOrDefault(x => x.Id == id);

            return View(orderAnonymous);
        }

        [HttpPost]
        public async Task<IActionResult> AddingDataAnonymous(Order order, int id)
        {
            if (ModelState.IsValid)
            {
                Order orderAnonymous = db
                .OrderUsers
                .FirstOrDefault(x => x.Id == id);

                orderAnonymous.StreetName = order.StreetName;
                orderAnonymous.PhoneNumber = order.PhoneNumber;
                orderAnonymous.HouseNumber = order.HouseNumber;
                orderAnonymous.ApartmentsNumber = order.ApartmentsNumber;
                orderAnonymous.City = order.City;
                orderAnonymous.Email = order.Email;
                orderAnonymous.PostCode = order.PostCode;
                orderAnonymous.Name = order.Name;
                orderAnonymous.IsOrdered = true;

                db.OrderUsers.Update(orderAnonymous);
                await db.SaveChangesAsync();
                return RedirectToAction("EndOrderAnonymous");
            }
            return View(order);
        }

        public IActionResult EndOrderAnonymous()
        {
            return View();
        }
    }
}
