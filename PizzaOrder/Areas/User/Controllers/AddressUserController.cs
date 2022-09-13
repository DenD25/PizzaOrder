using Microsoft.AspNetCore.Mvc;

namespace PizzaOrder.Areas.User.Controllers
{
    public class AddressUserController : Controller
    {
        public IActionResult AddressUser()
        {
            return View();
        }
    }
}
