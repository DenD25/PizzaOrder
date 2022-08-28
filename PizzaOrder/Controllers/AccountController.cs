using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaOrder.Models;
using System.Security.Claims;

namespace PizzaOrder.Controllers
{
    public class AccountController : Controller
    {
        ApplicationContext db;

        public AccountController(ApplicationContext context)
        {
            db = context;
        }

        // Register

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(x => x.Login == model.Login);
                if (user == null)
                {
                    user = new User {
                        Login = model.Login,
                        Password = model.Password,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.Email,
                        City = model.City,
                        ApartmentsNumber = model.ApartmentsNumber,
                        HouseNumber = model.HouseNumber,
                        StreetName = model.StreetName,
                        PostCode = model.PostCode
                    };
                    Role userRole = await db.Roles.FirstOrDefaultAsync(x => x.Name == "user");
                    if (userRole != null)
                        user.Role = userRole;

                    db.Users.Add(user);
                    await db.SaveChangesAsync();

                    await Authenticate(user);

                    return RedirectToRoute("userRoute", new { area = "User", controller = "Home", action = "Index" });
                }
                else
                    ModelState.AddModelError("", "Change login");
            }
            return View(model);
        }

        //Login

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users
                    .Include(x => x.Role)
                    .FirstOrDefaultAsync(x => x.Login == model.Login && x.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user);

                    if (user.Role.Name.ToString() == "admin")
                    {
                        return RedirectToRoute("adminRoute", new { area = "Admin", controller = "Main", action = "Index" });
                    }
                    if (user.Role.Name.ToString() == "manager")
                    {
                        return RedirectToRoute("managerRoute", new { area = "Manager", controller = "Main", action = "Index" });
                    }

                    return RedirectToRoute("userRoute", new { area = "User", controller = "Home", action = "Index" });
                }
                ModelState.AddModelError("", "Incorrect login or password");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Main");
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name),
                new Claim("Id", user.Id.ToString())
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
