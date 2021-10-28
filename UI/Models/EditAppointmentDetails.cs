using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models
{
    public class EditAppointmentDetails
    {
        public Guid AppointmentId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int AppointmentSlotId { get; set; }

    }
}