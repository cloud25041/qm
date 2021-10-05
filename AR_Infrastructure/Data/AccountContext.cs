using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AR_Domain.AggregateModel;
using AR_Domain.SeedWork;
using System.Threading;

namespace AR_Infrastructure.Data
{
    public class AccountContext : DbContext, IUnitOfWork
    {
        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Account>().HasKey(x => x.Username);
            modelBuilder.Entity<Account>().OwnsOne(x => x.AccountDetails);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            await base.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
