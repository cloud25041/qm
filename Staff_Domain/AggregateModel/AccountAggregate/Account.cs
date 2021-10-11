using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Staff_Domain.AggregateModel.AccountAggregate
{
    public class Account
    {
        public Guid AccountId { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public string Mobile { get; private set; }
        public Agency Agency { get; private set; }
        public Schedule Schedule { get; private set; }
    }
}
