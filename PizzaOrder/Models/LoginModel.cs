using System.ComponentModel.DataAnnotations;

namespace PizzaOrder.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Login is required")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
