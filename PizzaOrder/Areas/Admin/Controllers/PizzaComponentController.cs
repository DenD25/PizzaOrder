using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaOrder.Models;

namespace PizzaOrder.Areas.Admin.Controllers
{
    
    public class PizzaComponentController : Controller
    {
        ApplicationContext db;

        public PizzaComponentController(ApplicationContext context)
        {
            db = context;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PizzaComAdd()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PizzaComAdd(PizzaComponent pizzaComponent)
        {
            if (ModelState.IsValid)
            {
                db.PizzaComponents.Add(pizzaComponent);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return View(pizzaComponent);
            }
        }
    }
}
