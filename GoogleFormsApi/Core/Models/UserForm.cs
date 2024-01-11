using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class UserForm
    {
        public Guid Id { get; set; }

        public virtual ICollection<Response> Responses { get; set; }

        public virtual Guid UserId { get; set; }

        public virtual AppUser User { get; set; }

        public virtual Guid FormId { get; set; }

        public virtual Form Form { get; set; }
    }
}
