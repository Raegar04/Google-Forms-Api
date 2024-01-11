using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Question
    {
        public Guid Id { get; set; }

        public int Index { get; set; }

        public string Description { get; set; }

        public virtual Guid FormId { get; set; }

        public virtual Form Form { get; set; }

        public virtual ICollection<Response> Responses { get; set; }
    }
}
