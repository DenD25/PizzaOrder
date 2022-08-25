using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaOrder.Models;

namespace PizzaOrder.Areas.Admin.Controllers
{
    public class PizzaController : Controller
    {
        ApplicationContext db;

        public PizzaController(ApplicationContext context)
        {
            db = context;

        }
        public async Task<IActionResult> Index()
        {
            return View(await db.PizzaComponents.ToListAsync());
        }
    }
}
