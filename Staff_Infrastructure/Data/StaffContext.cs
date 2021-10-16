using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Staff_Domain.AggregateModel.AccountAggregate;
using Staff_Domain.AggregateModel.AppointmentAggregate;
using Staff_Domain.SeedWork;
using System.Threading;
using MediatR;
using Staff_Infrastructure;
using Staff_Domain.AggregateModel.AgencyAggregate;

namespace Staff_Infrastructure.Data
{
    public class StaffContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;
        public StaffContext(DbContextOptions<StaffContext> options) : base(options)
        {
        }
        public StaffContext(DbContextOptions<StaffContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Account");
            modelBuilder.Entity<Account>().HasKey(x => x.Username);
          
            modelBuilder.Entity<Account>().OwnsOne(x => x.Schedule);


            modelBuilder.Entity<Appointment>().ToTable("Appointment");
            modelBuilder.Entity<Agency>().ToTable("Agency");
            
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
