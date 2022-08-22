using System.ComponentModel.DataAnnotations;

namespace PizzaOrder.Models
{
    public class PizzaComponent
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public ICollection<Pizza>? Pizzas { get; set; }
    }
}
