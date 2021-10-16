using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AR_Application.Model
{
    public class UserAgencyInputViewModel
    {
        public int? AgencyId { get; set; }

        public int? ConcurrentUser { get; set; }

        public int? AppointmentTypeId { get; set; }

        public DateTime? SelectedDate { get; set; }
    }
}
