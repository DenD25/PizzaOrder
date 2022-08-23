using System.ComponentModel.DataAnnotations;

namespace PizzaOrder.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Login is required")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [DataType(DataType.PhoneNumber)]
        public int PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string? StreetName { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string? HouseNumber { get; set; }
        public int ApartmentsNumber { get; set; }
    }
}
