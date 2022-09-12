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

        public async Task<IActionResult> AddingPizzas(int id, int count, int pizzaId)
        {
            OrderUser orderUser = db
                .OrderUsers
                .Include(x => x.Pizzas)
                .Include(x => x.User)
                .FirstOrDefault(x => x.UserId == id);

            if (orderUser == null || orderUser.IsOrdered == true)
            {
                OrderUser newOrder = new OrderUser();

                newOrder.UserId = id;
                newOrder.User = db
                    .Users
                    .FirstOrDefault(x => x.Id == id);
                newOrder.Name = "Name";                
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

                return View(newOrder);
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

                return View(orderUser);
            }

            return View();
        }

        public IActionResult AddingData(int id)
        {
            Models.User user = db
                .Users
                .FirstOrDefault(x => x.Id == id);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddingData(Models.User user, int id, string name)
        {
            if (ModelState.IsValid)
            {
                OrderUser orderUser = db
                .OrderUsers
                .Include(x => x.User)
                .FirstOrDefault(x => x.UserId == id);

                orderUser.StreetName = user.StreetName;
                orderUser.PhoneNumber = user.PhoneNumber;
                orderUser.HouseNumber = user.HouseNumber;
                orderUser.ApartmentsNumber = user.ApartmentsNumber;
                orderUser.City = user.City;
                orderUser.Email = user.Email;
                orderUser.PostCode = user.PostCode;
                orderUser.Name = name;
                orderUser.IsOrdered = true;

                db.OrderUsers.Update(orderUser);
                await db.SaveChangesAsync();
                return RedirectToAction("EndOrder");
            }
            return View(user);
        }

        public async Task<IActionResult> EndOrder(Models.User user)
        {
            return View();
        }
    }
}
