using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Staff_Application.Queries
{
    public class ViewAllAppointmentViewModel
    {
        public Guid UserAccountId { get; init; }
        public string CustomerName { get; init; }
        public DateTime AppointmentDate { get; init; }

        public int AppointmentSlotID { get; init; }
        public Guid AppointmentId { get; init; }
    }
}
