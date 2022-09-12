using Microsoft.AspNetCore.Mvc;

namespace PizzaOrder.Areas.Manager.Controllers
{
    public class Order : Controller
    {
        public IActionResult OrderList()
        {


            return View();
        }
    }
}
