using Staff_Domain.AggregateModel.AccountAggregate;
using Staff_Domain.SeedWork;
using Staff_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Staff_Infrastructure.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private StaffContext _staffContext;

        public IUnitOfWork UnitOfWork { get { return _staffContext; } }

        public AccountRepository(StaffContext staffContext)
        {
            _staffContext = staffContext;
        }

        public Account Add(Account account)
        {
            return _staffContext.
                Accounts.Add(account).Entity;
        }

        public async Task<Account> GetAsync(string username)
        {
            var account = await _staffContext
                                .Accounts
                               
                                .FirstOrDefaultAsync(o => o.Username == username);
            if (account == null)
            {
                account = _staffContext
                            .Accounts
                            .Local
                            .FirstOrDefault(o => o.Username == username);
            }

            return account;
        }

        public async Task<Account> GetAccountByIdAsync(Guid id)
        {
            var account = await _staffContext
                                .Accounts

                                .FirstOrDefaultAsync(o => o.AccountId == id);
            if (account == null)
            {
                account = _staffContext
                            .Accounts
                            .Local
                            .FirstOrDefault(o => o.AccountId == id);
            }

            return account;
        }
    }
}
