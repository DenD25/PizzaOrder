using System.ComponentModel.DataAnnotations;

namespace PizzaOrder.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string? Password { get; set; }
        public int? RoleId { get; set; }
        public Role? Role { get; set; }
        public int PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
        public string? StreetName { get; set; }
        public string? HouseNumber { get; set; }
        public int ApartmentsNumber { get; set; }
        public int PostCode { get; set; }
    }
}
