using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Staff_API.Models
{
    public class PhysicalCounterDetails
    {
        public Guid appointmentId { get; set; }
        public Guid StaffId { get; set; }
        public int state { get; set; }
    }
}
