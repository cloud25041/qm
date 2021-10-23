using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffUI.Models
{
    public class LoginDetails
    {
        public Guid TransactionId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string AccountId { get; set; }
        public int AgencyId { get; set; }
        public bool IsLoginSuccessful { get; set; }

    }
}
