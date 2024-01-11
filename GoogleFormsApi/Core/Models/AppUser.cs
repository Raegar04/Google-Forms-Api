using Microsoft.AspNetCore.Identity;

namespace Core.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public virtual ICollection<Form> HoldedForms { get; set; }

        public virtual ICollection<UserForm> AssignedForms { get; set; }
    }
}
