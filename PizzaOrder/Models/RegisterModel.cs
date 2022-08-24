using System.ComponentModel.DataAnnotations;

namespace PizzaOrder.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Login is required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Passwords don`t match")]
        public string? ConfirmPassword { get; set; }

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
    }
}
