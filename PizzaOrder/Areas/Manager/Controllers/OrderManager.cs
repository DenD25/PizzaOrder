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
            List<Order> order = db
                .OrderUsers
                .Where(x => x.IsOrdered == true)
                .Include(x => x.Pizzas)
                .ToList();
                
            return View(order);
        }
    }
}
