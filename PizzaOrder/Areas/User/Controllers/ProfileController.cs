using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaOrder.Models;

namespace PizzaOrder.Areas.User.Controllers
{
    [Authorize(Roles = "user")]
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

        public async Task<IActionResult> ProfileEdit(int id)
        {
            return View(await db.Users.FirstOrDefaultAsync(x => x.Id == id));
        }

        [HttpPost]
        public async Task<IActionResult> ProfileEdit(Models.User model, int id)
        {          
            if (ModelState.IsValid)
            {
                Models.User user = db.Users.FirstOrDefault(x => x.Id == id);
                user.Login = model.Login;
                user.Email = model.Email;
                user.City = model.City;
                user.HouseNumber = model.HouseNumber;
                user.ApartmentsNumber = model.ApartmentsNumber;
                user.StreetName = model.StreetName;
                user.PostCode = model.PostCode;

                db.Users.Update(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(model);
            }
        }
    }
}
