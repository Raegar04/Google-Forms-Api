using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class AppUser : IdentityUser<Guid>
    {
        public virtual ICollection<Form> HoldedForms { get; set; }

        public virtual IOrderedEnumerable<UserForm> AssignedForms { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}
