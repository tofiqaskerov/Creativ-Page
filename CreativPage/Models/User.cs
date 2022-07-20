using Microsoft.AspNetCore.Identity;

namespace CreativPage.Models
{
    public class User : IdentityUser
    {
        public string Fullname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
