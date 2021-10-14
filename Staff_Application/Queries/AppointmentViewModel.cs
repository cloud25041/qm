using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Staff_Application.Queries
{
    public record AppointmentViewModel
    {
        public Guid AppointmentId { get; init; }
        public Guid AccountId { get; init; }
        public int AppointmentState { get; init; }
        public string AgencyCode { get; init; }
        public DateTime StartTime { get; init; }
        public DateTime EndTime { get; init; }
    }
}
