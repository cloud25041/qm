using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffUI.Models
{
    public class AccountDetails
    {
        public Guid TransactionId { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }


    }
}
