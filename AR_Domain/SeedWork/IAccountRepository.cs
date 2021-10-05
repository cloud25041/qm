using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AR_Domain.AggregateModel.AccountAggregate;

namespace AR_Domain.SeedWork
{
    public interface IAccountRepository
    {
        public IUnitOfWork UnitOfWork { get; }
        public Account Add(Account account);
    }
}
