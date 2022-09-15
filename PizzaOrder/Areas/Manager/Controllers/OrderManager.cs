using Microsoft.AspNetCore.Mvc;
using PizzaOrder.Models;
using Microsoft.EntityFrameworkCore;

namespace PizzaOrder.Areas.Manager.Controllers
{
    public class OrderManager : Controller
    {
        ApplicationContext db;

        public OrderManager(ApplicationContext context)
        {
            db = context;
        }
        public IActionResult OrderList()
        {
            List<Order> orders = db
                .OrderUsers
                .Where(x => x.IsOrdered == true)
                .Where(x => x.IsCooked == false)
                .Include(x => x.Pizzas)
                .ToList();
                
            return View(orders);
        }

        public IActionResult OrderCooked(int id)
        {
            Order order = db
                .OrderUsers
                .FirstOrDefault(x => x.Id == id);

            order.IsCooked = true;

            db.OrderUsers.Update(order);
            db.SaveChanges();

            return RedirectToAction("OrderList");
        }

        public IActionResult OrderDeliveryList()
        {
            List<Order> orders = db
                .OrderUsers
                .Where(x => x.IsCooked == true)
                .Include(x => x.Pizzas)
                .ToList();

            return View(orders);
        }

        public IActionResult OrderDelivered(int id)
        {
            Order order = db
                .OrderUsers
                .FirstOrDefault(x => x.Id == id);

            order.IsDelivered = true;
            order.EndTime = DateTime.Now;

            db.OrderUsers.Update(order);
            db.SaveChanges();

            return RedirectToAction("OrderDeliveryList");
        }

        public IActionResult OrderDone()
        {
            List<Order> orders = db
                .OrderUsers
                .Where(x => x.IsDelivered == true)
                .Include(x => x.Pizzas)
                .ToList();

            return View(orders);
        }
    }
}
