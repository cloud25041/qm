using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AR_API.Models
{
    public class UserAgencyInput
    {
        public int? AgencyId { get; set; }

        public int? ConcurrentUser { get; set; }

        public int? AppointmentTypeId { get; set; }

        public DateTime? SelectedDate { get; set; }
    }
}
