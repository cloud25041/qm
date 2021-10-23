using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffUI.Models
{
    public class UserAccountInfo
    {
        public string username { get; set; }
        public Guid accountId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int mobileNo { get; set; }


    }
}
