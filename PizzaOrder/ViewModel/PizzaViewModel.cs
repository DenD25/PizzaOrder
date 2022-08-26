using PizzaOrder.Models;

namespace PizzaOrder.ViewModel
{
    public class PizzaViewModel
    {
        public Pizza Pizza { get; set; }
        public IEnumerable<PizzaComponent> PizzaComponents { get; set; }
    }
}
