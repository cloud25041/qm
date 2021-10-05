using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AR_Domain.SeedWork;

namespace AR_Domain.AggregateModel.AccountAggregate
{
    public interface IAccountRepository : IRepository<Account>
    {
        public Account Add(Account account);
        public Task<Account> GetAsync(string username);
    }
}
