using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [PrimaryKey("Id")]
    public class Photo
    {
        public string Id { get; set; }

        public string Url { get; set; }

        public bool IsMain { get; set; }

        public virtual Guid AppUserId { get; set; }

        public virtual AppUser AppUser { get; set; }
    }
}
