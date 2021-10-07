using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models
{
    public class VerificationCodeDetails
    {
        public Guid TransactionId { get; set; }
        public string VerificationCode { get; set; }
    }
}
