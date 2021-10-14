using Staff_Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Staff_Domain.AggregateModel.AccountAggregate
{


    public class Account: IAggregateRoot
    {
        public Account(string username, string password, string name, string email, int mobile)
        {
           
            Username = username;
            Password = password;
            Name = name;
            Email = email;
            Mobile = mobile;
           
        }

        public Guid AccountId { get; private set; }
        public string Name { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public int Mobile { get; private set; }
        public Agency Agency { get; private set; }
        public Schedule Schedule { get; private set; }


        
    }
}
