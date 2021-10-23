using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffUI.Models
{
    public class StaffAppointmentDetails
    {
        public Guid AppointmentId { get; set; }
        public int AppointmentType { get; set; }
        public int AppointmentState { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int AppointmentSlotId { get; set; }
        public Guid UserAccountId { get; set; }
        public Guid StaffAccountID { get; set; }
        public int AgencyId { get; set; }
        public string ZoomLink { get; set; }

    }
}
