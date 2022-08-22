using System.ComponentModel.DataAnnotations;

namespace PizzaOrder.Models
{
    public class Pizza
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public float Price { get; set; }
        public ICollection<PizzaComponent>? PizzaComponents { get; set; }
    }
}
