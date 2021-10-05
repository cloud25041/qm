using AR_Domain.AggregateModel.AccountAggregate;
using AR_Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AR_Domain.SeedWork;

namespace AR_Infrastructure.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private AccountContext _accountContext;

        public IUnitOfWork UnitOfWork { get { return _accountContext; } }

        public AccountRepository(AccountContext accountContext)
        {
            _accountContext = accountContext;
        }

        public Account Add(Account account)
        {
            return _accountContext.Accounts.Add(account).Entity;
        }

        public async Task<Account> GetAsync(string username)
        {
            var account = await _accountContext
                                .Accounts
                                .Include(x => x.AccountDetails)
                                .FirstOrDefaultAsync(o => o.Username == username);
            if (account == null)
            {
                account = _accountContext
                            .Accounts
                            .Local
                            .FirstOrDefault(o => o.Username == username);
            }

            return account;
        }
    }
}
