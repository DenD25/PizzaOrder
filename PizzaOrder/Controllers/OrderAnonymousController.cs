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
        public async Task<IActionResult> Index( int? id, int count, int pizzaId)
        {
            Order ordercheck = db
                .OrderUsers
                .Include(x => x.Pizzas)
                .Where(x => x.IsOrdered == false)
                .FirstOrDefault(x => x.Id == id);

            if(ordercheck != null)
            {
                Pizza orderPizza = db
                    .Pizzas
                    .FirstOrDefault(x => x.Id == pizzaId);

                List<Pizza> pizzaList = ordercheck.Pizzas.ToList();

                orderPizza.Count = count;

                pizzaList.Add(orderPizza);

                ordercheck.Pizzas = pizzaList;

                db.OrderUsers.Update(ordercheck);
                await db.SaveChangesAsync();

                return RedirectToAction("AddingPizzasAnonymous", new { id = ordercheck.Id });
            }
            else
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

            ViewData["OrderId"] = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddingDataAnonymous(UserAnonymous user, int id)
        {
            if (ModelState.IsValid)
            {
                Order orderAnonymous = db
                .OrderUsers
                .FirstOrDefault(x => x.Id == id);

                orderAnonymous.StreetName = user.StreetName;
                orderAnonymous.PhoneNumber = user.PhoneNumber;
                orderAnonymous.HouseNumber = user.HouseNumber;
                orderAnonymous.ApartmentsNumber = user.ApartmentsNumber;
                orderAnonymous.City = user.City;
                orderAnonymous.Email = user.Email;
                orderAnonymous.PostCode = user.PostCode;
                orderAnonymous.Name = user.Name;
                orderAnonymous.IsOrdered = true;
                orderAnonymous.Delivering = true;

                db.OrderUsers.Update(orderAnonymous);
                await db.SaveChangesAsync();
                return RedirectToAction("EndOrderAnonymous", new { id = orderAnonymous.Id });
            }
            return View(user);
        }

        public IActionResult AddingDataSpotAnonymous(int id)
        {
            ViewData["OrderId"] = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddingDataSpotAnonymous(UserAnonymous user, int id)
        {
            if (user.PhoneNumber != null && user.Name != null)
            {
                Order orderAnonymous = db
                .OrderUsers
                .FirstOrDefault(x => x.Id == id);

                orderAnonymous.PhoneNumber = user.PhoneNumber;
                orderAnonymous.Name = user.Name;
                orderAnonymous.IsOrdered = true;
                orderAnonymous.Delivering = false;

                db.OrderUsers.Update(orderAnonymous);
                await db.SaveChangesAsync();
                return RedirectToAction("EndOrderAnonymous", new { id = orderAnonymous.Id });
            }
            return View(user);
        }

        public IActionResult EndOrderAnonymous(int id)
        {
            Order order = db
                .OrderUsers
                .FirstOrDefault(x => x.Id == id);
            return View(order);
        }

        public async Task<IActionResult> DeletingOrderAnonymous(int id)
        {
            Order order = db
                .OrderUsers
                .FirstOrDefault(x => x.Id == id);

            db.OrderUsers.Remove(order);
            db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
