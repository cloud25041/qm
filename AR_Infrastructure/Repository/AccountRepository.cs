using AR_Domain.AggregateModel.AccountAggregate;
using AR_Domain.SeedWork;
using AR_Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<Account> GetAsync(string username)
        {
            var account = await _customerContext
                                .Accounts
                                .Include(x => x.AccountDetails)
                                .FirstOrDefaultAsync(o => o.Username == username);
            if (account == null)
            {
                account = _customerContext
                            .Accounts
                            .Local
                            .FirstOrDefault(o => o.Username == username);
            }

            return account;
        }
    }
}
