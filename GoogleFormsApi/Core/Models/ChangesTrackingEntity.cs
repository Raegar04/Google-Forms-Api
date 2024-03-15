using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public abstract class ChangesTrackingEntity : IComparable<ChangesTrackingEntity>
    {
        public DateTime LastUpdated { get; set; }

        public Guid? UpdatedByUserWithId { get; set; }

        public int CompareTo(ChangesTrackingEntity? other)
        {
            return LastUpdated.CompareTo(other?.LastUpdated);
        }
    }
}
