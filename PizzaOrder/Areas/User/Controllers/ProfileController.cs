using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaOrder.Models;

namespace PizzaOrder.Areas.User.Controllers
{
    public class ProfileController : Controller
    {
        ApplicationContext db;

        public ProfileController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index(int id)
        {
             return  View(await db.Users.FirstOrDefaultAsync(x => x.Id == id));
        }
    }
}
