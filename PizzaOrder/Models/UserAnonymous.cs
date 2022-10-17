using System.ComponentModel.DataAnnotations;

namespace PizzaOrder.Models
{
    public class UserAnonymous
    {
        [Required(ErrorMessage = "Please, write your name.")]
        [StringLength(30)]
        public string? Name { get; set; }
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
