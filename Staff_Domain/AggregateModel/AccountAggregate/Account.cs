using Staff_Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Staff_Domain.AggregateModel.AccountAggregate
{


    public class Account: IAggregateRoot
    {
        public Account(Guid accountId, string name, string username, string password, string email, int mobile, int agencyId)
        {
            AccountId = accountId;
            Name = name;
            Username = username;
            Password = password;
            Email = email;
            Mobile = mobile;
            AgencyId = agencyId;
            Schedule = new Schedule();
        }

        public Guid AccountId { get; private set; }
        public string Name { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public int Mobile { get; private set; }
        public int AgencyId { get; private set; }
        public Schedule Schedule { get; private set; }


        
    }
}
