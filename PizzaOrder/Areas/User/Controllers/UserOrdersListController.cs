using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaOrder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaOrder.Models;
using Microsoft.AspNetCore.Authorization;

namespace PizzaOrder.Areas.User.Controllers
{
    [Authorize(Roles = "user")]
    public class UserOrdersListController : Controller
    {
        ApplicationContext db;

        public UserOrdersListController(ApplicationContext context)
        {
            db = context;
        }

        public IActionResult Orders(int id)
        {
            List<Order> order = db
                .OrderUsers
                .Where(x => x.IsOrdered == true)
                .Where(x => x.UserId == id)
                .Include(x => x.Pizzas)
                .ToList();

            return View(order);
        }
    }
}
