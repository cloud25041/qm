using Staff_Domain.AggregateModel.AppointmentAggregate;
using Staff_Domain.SeedWork;
using Staff_Infrastructure.Data;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            return _staffContext.Appointments.Add(appointment).Entity;
        }

        public Appointment Update(Appointment appointment)
        {
            return _staffContext.Appointments.Update(appointment).Entity;
        }

        public async Task<Appointment> GetAppointmentByIdAsync(Guid id)
        {
            var appointment = await _staffContext
                                .Appointments
                                .FirstOrDefaultAsync(o => o.AppointmentId == id);

            if (appointment == null)
            {
                appointment = _staffContext
                            .Appointments
                            .Local
                            .FirstOrDefault(o => o.AppointmentId == id);
            }

            return appointment;
        }
    }
}
