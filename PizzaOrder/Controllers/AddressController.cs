using Microsoft.AspNetCore.Mvc;

namespace PizzaOrder.Controllers
{
    public class AddressController : Controller
    {
        public IActionResult Address()
        {
            return View();
        }
    }
}
