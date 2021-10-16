using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AR_Domain.AggregateModel.AccountAggregate;
using AR_Domain.AggregateModel.AppointmentAggregate;
using AR_Domain.SeedWork;
using System.Threading;
using MediatR;

namespace AR_Infrastructure.Data
{
    public class CustomerContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {
        }
        public CustomerContext(DbContextOptions<CustomerContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Account>().HasKey(x => x.Username);
            modelBuilder.Entity<Account>().OwnsOne(x => x.AccountDetails);

            modelBuilder.Entity<Appointment>().ToTable("Appointment");
            modelBuilder.Entity<Appointment>().HasKey(a => a.AppointmentId);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            await _mediator.DispatchDomainEventsAsync(this);
            await base.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
