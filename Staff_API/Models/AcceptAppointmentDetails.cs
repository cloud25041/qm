using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Staff_API.Models
{
    public class AcceptAppointmentDetails
    {
        public Guid AccountId { get; set; }
        public Guid AppointmentId { get; set; }
    }
}
