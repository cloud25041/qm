using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AR_API.Models
{
    public class CreateAppointmentDetails
    {
        public int AgencyId { get; set; }
        public int AppointmentType { get; set; }
        public int AppointmentState { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int AppointmentSlotId { get; set; }
        public Guid UserAccountId { get; set; }
    }
}

