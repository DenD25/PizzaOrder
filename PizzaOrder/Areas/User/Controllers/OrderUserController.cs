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
        public IActionResult Index(int id)
        {
            List<Pizza> pizzas = db.Pizzas.ToList();
            List<PizzaComponent> pizzaComponents = db.PizzaComponents.ToList();
            Order order = db
                .OrderUsers
                .Where(x => x.IsOrdered == false)
                .FirstOrDefault(x => x.UserId == id);

            OrderUserViewModel ouvm = new OrderUserViewModel { OrderUser = order, PizzaComponents = pizzaComponents, Pizzas = pizzas };

            return View(ouvm);
        }


        [HttpPost]
        public async Task<IActionResult> Index(int id, int count, int pizzaId)
        {
            Order orderUser = db
                .OrderUsers
                .Include(x => x.Pizzas)
                .Where(x => x.IsOrdered == false)
                .FirstOrDefault(x => x.UserId == id);

            if (orderUser == null || orderUser.IsOrdered == true)
            {
                Order newOrder = new Order();

                newOrder.UserId = id;
                newOrder.User = db
                    .Users
                    .FirstOrDefault(x => x.Id == id);           
                newOrder.CreateTime = DateTime.Now;

                Pizza orderPizza = db
                    .Pizzas
                    .FirstOrDefault(x => x.Id == pizzaId);

                List<Pizza> pizzaList = new List<Pizza>();

                orderPizza.Count = count;

                pizzaList.Add(orderPizza);               

                newOrder.Pizzas = pizzaList;

                db.OrderUsers.Add(newOrder);
                await db.SaveChangesAsync();

                return RedirectToAction("AddingPizzas", new { id = newOrder.Id });
            }

            else if(orderUser.IsOrdered == false)
            {
                Pizza orderPizza = db
                    .Pizzas
                    .FirstOrDefault(x => x.Id == pizzaId);

                List<Pizza> pizzaList = orderUser.Pizzas.ToList();

                orderPizza.Count = count;

                pizzaList.Add(orderPizza);

                orderUser.Pizzas = pizzaList;   

                db.OrderUsers.Update(orderUser);
                await db.SaveChangesAsync();

                return RedirectToAction("AddingPizzas", new { id = orderUser.Id });
            }

            return View();
        }

        public IActionResult AddingPizzas(int id)
        {
            Order order = db
                .OrderUsers
                .Include(x => x.Pizzas)
                .FirstOrDefault(x => x.Id == id);

            return View(order);
        }

        public IActionResult AddingData(int id)
        {
            Models.User user = db
                .Users
                .FirstOrDefault(x => x.Id == id);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddingData(Models.User user, int id)
        {
            if (ModelState.IsValid)
            {
                Order orderUser = db
                .OrderUsers
                .Include(x => x.User)
                .Where(x => x.IsOrdered == false)
                .FirstOrDefault(x => x.UserId == id);

                orderUser.StreetName = user.StreetName;
                orderUser.PhoneNumber = user.PhoneNumber;
                orderUser.HouseNumber = user.HouseNumber;
                orderUser.ApartmentsNumber = user.ApartmentsNumber;
                orderUser.City = user.City;
                orderUser.Email = user.Email;
                orderUser.PostCode = user.PostCode;
                orderUser.Name = user.Name;
                orderUser.IsOrdered = true;
                orderUser.Delivering = true;

                db.OrderUsers.Update(orderUser);
                await db.SaveChangesAsync();
                return RedirectToAction("EndOrder", new { id = orderUser.Id });
            }
            return View(user);
        }

        public IActionResult AddingDataSpot(int id)
        {
            Models.User user = db
                .Users
                .FirstOrDefault(x => x.Id == id);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddingDataSpot(Models.User user, int id)
        {
            if (user.Name != null && user.PhoneNumber != null)
            {
                Order orderUser = db
                .OrderUsers
                .Include(x => x.User)
                .Where(x => x.IsOrdered == false)
                .FirstOrDefault(x => x.UserId == id);

                orderUser.PhoneNumber = user.PhoneNumber;
                orderUser.Email = user.Email;
                orderUser.Name = user.Name;
                orderUser.IsOrdered = true;
                orderUser.Delivering = false;

                db.OrderUsers.Update(orderUser);
                await db.SaveChangesAsync();
                return RedirectToAction("EndOrder", new { id = orderUser.Id });
            }
            return View(user);
        }

        public IActionResult EndOrder(int? id)
        {
            Order order = db
                .OrderUsers
                .FirstOrDefault(x => x.Id == id);
            return View(order);
        }

        public async Task<IActionResult> DeletingOrder(int id)
        {
            List<Order> order= db
                .OrderUsers
                .Where(x => x.IsOrdered == false)
                .Where(x => x.UserId == id)
                .ToList();

            foreach(Order item in order)
            {
                db.OrderUsers.Remove(item);
            }
            
            db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
