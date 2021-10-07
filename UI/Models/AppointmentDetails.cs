using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models
{
    public class AppointmentDetails
    {
        public Guid TransactionId { get; set; }

        public string AppointmentName { get; set; }

        public DateTime AppointmentTime { get; set; }
        public string Agency { get; set; }
        public string Service { get; set; }
        public string Location { get; set; }
        public int AppointmentDetailId { get; set; }

    }
}
