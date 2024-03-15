using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Responses
{
    public class UserDetailsResponse
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public ICollection<Photo> Photos { get; set; }
    }
}
