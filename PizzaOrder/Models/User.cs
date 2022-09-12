using System.ComponentModel.DataAnnotations;

namespace PizzaOrder.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Login is required")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Please, write your name.")]
        [StringLength(30)]
        public string? Name { get; set; }
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public int? RoleId { get; set; }
        public Role? Role { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [DataType(DataType.PhoneNumber)]
        public int PhoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string? City { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string? StreetName { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string? HouseNumber { get; set; }
        public string? ApartmentsNumber { get; set; }
        public string? PostCode { get; set; }
        public ICollection<Order>? OrderUsers { get; set; }
    }
}
