using Microsoft.AspNetCore.Mvc;

namespace PizzaOrder.Areas.User.Controllers
{
    public class Main : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
