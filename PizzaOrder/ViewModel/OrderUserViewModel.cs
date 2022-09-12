using PizzaOrder.Models;

namespace PizzaOrder.ViewModel
{
    public class OrderUserViewModel
    {
        public Order OrderUser { get; set; }
        public IEnumerable<Pizza> Pizzas { get; set; }
        public IEnumerable<PizzaComponent> PizzaComponents { get; set; }
    }
}
