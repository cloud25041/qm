using AR_Domain.AggregateModel.AccountAggregate;
using AR_Domain.SeedWork;
using AR_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace AR_Infrastructure.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private CustomerContext _customerContext;

        public IUnitOfWork UnitOfWork { get { return _customerContext; } }

        public AccountRepository(CustomerContext customerContext)
        {
            _customerContext = customerContext;
        }

        public Account Add(Account account)
        {
            return _customerContext.Accounts.Add(account).Entity;
        }

        public async Task<Account> GetAccountByIdAsync(Guid id)
        {
            var account = await _customerContext
                                .Accounts
                                .FirstOrDefaultAsync(o => o.AccountID == id);
            if (account == null)
            {
                account = _customerContext
                            .Accounts
                            .Local
                            .FirstOrDefault(o => o.AccountID == id);
            }

            return account;
        }
    }
}
