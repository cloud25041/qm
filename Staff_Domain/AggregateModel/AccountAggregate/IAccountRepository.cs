using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Staff_Domain.SeedWork;

namespace Staff_Domain.AggregateModel.AccountAggregate
{
    public interface IAccountRepository : IRepository<Account>
    {
        public Account Add(Account account);
        public Task<Account> GetAsync(string username);
        public Task<Account> GetAccountByIdAsync(Guid id);
    }
}
