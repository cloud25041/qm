using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AR_API.Models
{
    public class AvailableSlots
    {

        public DateTime? Date { get; set; }

        public string? Time { get; set; }

        public int SlotId { get; set; }
    }
}
