using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> Index()
        {
            return View(await db.PizzaComponents.ToListAsync());
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

        public IActionResult PizzaComEdit(int id)
        {
            return View(db.PizzaComponents.FirstOrDefault(x => x.Id == id));
        }

        [HttpPost]
        public async Task<IActionResult> PizzaComEdit(PizzaComponent pizzaComponent)
        {
            if (ModelState.IsValid)
            {
                db.PizzaComponents.Update(pizzaComponent);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return View(pizzaComponent);
            }
        }
        public IActionResult PizzaComDelete(int id)
        {
            return View(db.PizzaComponents.FirstOrDefault(x => x.Id == id));
        }

        [HttpPost]
        public async Task<IActionResult> PizzaComDelete(PizzaComponent pizzaComponent)
        {
            db.PizzaComponents.Remove(pizzaComponent);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
