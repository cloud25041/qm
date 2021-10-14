using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models
{
    public class AppointmentModel
    {
        public Guid AppointmentId { get; set; }
        public Guid AccountId { get; set; }
        public int AppointmentState { get; set; }
        public string AgencyCode { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
