
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class RegisterUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string phone { get; set; }
        public string Address { get; set; }
    }
}
