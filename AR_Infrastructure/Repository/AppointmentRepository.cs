using AR_Domain.AggregateModel.AppointmentAggregate;
using AR_Domain.SeedWork;
using AR_Infrastructure.Data;
using System;

namespace AR_Infrastructure.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        public AppointmentRepository(CustomerContext customerContext)
        {
            _customerContext = customerContext;
        }

        private readonly CustomerContext _customerContext;
        public IUnitOfWork UnitOfWork { get { return _customerContext; } }

        public Appointment Add(Appointment appointment)
        {
            return _customerContext.Appointments.Add(appointment).Entity;
        }
    }
}
