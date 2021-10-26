using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffUI.DateTimeSlotHelper
{
    public class DateTimeSlotHelper
    {
        private readonly List<(int, string)> listOfTuple;

        public DateTimeSlotHelper()
        {
            if (listOfTuple == null)
            {
                listOfTuple = new List<(int, string)>();
                listOfTuple.Add((1, "0900 hrs"));
                listOfTuple.Add((2, "0930 hrs"));
                listOfTuple.Add((3, "1000 hrs"));
                listOfTuple.Add((4, "1030 hrs"));
                listOfTuple.Add((5, "1100 hrs"));
                listOfTuple.Add((6, "1130 hrs"));
                listOfTuple.Add((7, "1200 hrs"));
                listOfTuple.Add((8, "1230 hrs"));
                listOfTuple.Add((9, "1300 hrs"));
                listOfTuple.Add((10, "1330 hrs"));
                listOfTuple.Add((11, "1400 hrs"));
                listOfTuple.Add((12, "1430 hrs"));
                listOfTuple.Add((13, "1500 hrs"));
                listOfTuple.Add((14, "1530 hrs"));
                listOfTuple.Add((15, "1600 hrs"));
                listOfTuple.Add((16, "1630 hrs"));
            }
        }


        public int GetSlotByTime(string time)
        {
            foreach (var tuple in listOfTuple)
            {
                if (tuple.Item2 == time)
                {
                    return tuple.Item1;                   
                }
            }
            return 0;
        }

        public string GetTimebySlot(int slotId)
        {
            foreach (var tuple in listOfTuple)
            {
                if (tuple.Item1 == slotId)
                {
                    return tuple.Item2;
                }
            }
            return "";
        }
    }
}
