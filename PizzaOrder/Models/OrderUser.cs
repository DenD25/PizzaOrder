using System.ComponentModel.DataAnnotations;

namespace PizzaOrder.Models
{
    public class OrderUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public User? User { get; set; }
        [Required(ErrorMessage = "Please, write your name.")]
        [StringLength(30)]
        public string? Name { get; set; }
        public ICollection<Pizza>? Pizzas { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool? IsOrdered { get; set; } = false;
        public bool? IsCooked { get; set; } = false;
        public bool? IsDelivered { get; set; } = false;
        public int PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
        public string? StreetName { get; set; }
        public string? HouseNumber { get; set; }
        public string? ApartmentsNumber { get; set; }
        public string? PostCode { get; set; }
    }
}
