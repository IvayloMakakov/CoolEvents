using Microsoft.AspNetCore.Identity;

namespace CoolEvents.Models
{
    public class ApplicationUser:IdentityUser
    {
        //public string UserName { get; set; }
        //public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Ticket>  Tickets { get; set; }
    }
}
