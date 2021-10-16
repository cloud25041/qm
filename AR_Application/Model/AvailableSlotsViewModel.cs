using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AR_Application.Model
{
    public class AvailableSlotsViewModel
    {
        public DateTime? Date { get; init; }

        public string? Time { get; init; }

        public int SlotId { get; init; }
    }
}
