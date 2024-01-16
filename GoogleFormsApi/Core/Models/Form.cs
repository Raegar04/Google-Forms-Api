using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Form
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual Guid HolderId { get; set; }

        public virtual AppUser Holder { get; set; }

        public virtual ICollection<UserForm> AssignedUsers { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
