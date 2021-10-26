using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AR_Application.Queries
{
    public record AppointmentViewModel
    {
        public Guid AppointmentId { get; init; }
        public int AppointmentType { get; init; }
        public int AppointmentState { get; init; }
        public DateTime AppointmentDate { get; init; }
        public int AppointmentSlotId { get; init; }
        public Guid UserAccountId { get; init; }
        public Guid StaffAccountID { get; init; }
        public int AgencyId { get; init; }
        public string ZoomLink { get; init; }
    }
}
