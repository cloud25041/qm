using AR_Domain.AggregateModel.AppointmentAggregate;
using AR_Domain.SeedWork;
using AR_Infrastructure.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        public Appointment Update(Appointment appointment)
        {
            return _customerContext.Appointments.Update(appointment).Entity;
        }

        public async Task<Appointment> GetAppointmentByIdAsync(Guid id)
        {
            var appointment = await _customerContext
                                .Appointments
                                .FirstOrDefaultAsync(o => o.AppointmentId == id);

            if (appointment == null)
            {
                appointment = _customerContext
                            .Appointments
                            .Local
                            .FirstOrDefault(o => o.AppointmentId == id);
            }

            return appointment;
        }
    }
}
