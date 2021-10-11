using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AR_API.Models
{
    public class ValidationDetails
    {
       public bool IsUsernameTaken { get; set; }
        public bool? IsStaffKeyCorrect { get; set; }
        public bool IsEmailTaken { get; set; }


    }
}

