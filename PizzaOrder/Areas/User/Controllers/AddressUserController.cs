using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PizzaOrder.Areas.User.Controllers
{
    [Authorize(Roles = "user")]
    public class AddressUserController : Controller
    {
        public IActionResult AddressUser()
        {
            return View();
        }
    }
}
