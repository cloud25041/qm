using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models
{
    public class SignUpAccountDetails
    {
        public Guid TransactionId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string? StaffKey { get; set; }
        public bool isUser { get; set; }

        public bool? isKiosk { get; set; }
    }
}
