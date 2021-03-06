using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Staff_Domain.SeedWork;

namespace Staff_Domain.AggregateModel.AppointmentAggregate
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        public Appointment Add(Appointment appointment);
        public Appointment Update(Appointment appointment);
        public Task<Appointment> GetAppointmentByIdAsync(Guid id);
    }
}
