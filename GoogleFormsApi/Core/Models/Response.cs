using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Response
    { 
        public Guid Id { get; set; }

        public string Value { get; set; }

        public virtual Guid QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public virtual Guid UserFormId { get; set; }

        public virtual UserForm UserForm { get; set; }
    }
}
