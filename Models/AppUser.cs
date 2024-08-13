using Microsoft.AspNetCore.Identity;

namespace Nero.Models
{
    public class AppUser:IdentityUser
    {
        public string Address {  get; set; }
    }
}
