using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffUI.Models
{
    public class ViewAppointmentDetails
    {
        public Guid UserAccountId { get; set; }
        public string CustomerName { get; set; }
        public DateTime AppointmentDate { get; set; }

        public string AppointmentTime { get; set; }
        public int AppointmentSlotID { get; set; }
        public Guid AppointmentId { get; set; }

    }
}
