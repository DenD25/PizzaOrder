using Microsoft.AspNetCore.Mvc;

namespace PizzaOrder.Areas.User.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
