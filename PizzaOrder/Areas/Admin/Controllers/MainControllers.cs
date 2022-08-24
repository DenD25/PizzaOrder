using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PizzaOrder.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class MainControllers : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
