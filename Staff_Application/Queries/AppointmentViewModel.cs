using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Staff_Application.Queries
{
    public record AppointmentViewModel
    {
        public Guid AppointmentId { get; init; }
        public int AgencyId { get; init; }
        public Guid? StaffAccountId { get; init; }
        public DateTime AppointmentDate { get; init; }
        public int AppointmentSlotId { get; init; }
        public Guid CustomerId { get; init; }
        public string CustomerName { get; init; }
        public int AppointmentState { get; init; }
       public string ZoomId { get; init; }
       
    }
}
