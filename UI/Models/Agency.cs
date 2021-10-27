using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models
{
    public class Agency
    {
        public string AgencyName { get; set; }
        public int AgencyId { get; set; }
        public int PhysicalConcurrentUser { get; set; }
        public int VirtualConcurrentUser { get; set; }


    }
}
