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
                newOrder.Name = "None";                
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
            OrderUser orderUser = db
                .OrderUsers
                .Include(x => x.Pizzas)
                .Include(x => x.User)
                .FirstOrDefault(x => x.UserId == id);

            return View(orderUser);
        }
    }
}
