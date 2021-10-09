using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AR_Domain.SeedWork;

namespace AR_Domain.AggregateModel.AccountAggregate
{
    public class Account : IAggregateRoot
    {
        public Guid AccountID { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public AccountDetail AccountDetails { get; private set; }

        Account()
        {

        }

        public Account(string username, string password, string name, string email, int mobile)
        {
            this.AccountID = Guid.NewGuid();
            this.Username = username;
            this.Password = password;
            this.AccountDetails = new AccountDetail(name, email, mobile);
        }

        public Account ChangeUsername(string newUsername)
        {
            throw new NotImplementedException();
        }

        public Account CreateAccount()
        {
            throw new NotImplementedException();
        }
        public Account DeleteAccount()
        {
            throw new NotImplementedException();
        }
    }
}
