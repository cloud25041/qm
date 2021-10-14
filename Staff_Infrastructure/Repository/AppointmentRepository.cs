using Staff_Domain.AggregateModel.AppointmentAggregate;
using Staff_Domain.SeedWork;
using Staff_Infrastructure.Data;
using System;

namespace Staff_Infrastructure.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        public AppointmentRepository(StaffContext staffContext)
        {
            _staffContext = staffContext;
        }

        private readonly StaffContext _staffContext;
        public IUnitOfWork UnitOfWork { get { return _staffContext; } }

        public Appointment Add(Appointment appointment)
        {
            return _staffContext.
                Appointments.Add(appointment).Entity;
        }
    }
}
