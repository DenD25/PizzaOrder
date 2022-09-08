using System.ComponentModel.DataAnnotations;

namespace PizzaOrder.Models
{
    public class Pizza
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        [Required]
        public float Price { get; set; }
        public string? PhotoName { get; set; }
        public string? PhotoPath { get; set; }
        public int? Count { get; set; } = 1;
        public ICollection<PizzaComponent>? PizzaComponents { get; set; }
        public ICollection<OrderUser>? OrderUsers { get; set; }
        public ICollection<OrderAnonymous>? OrderAnonymous { get; set; }
    }
}
