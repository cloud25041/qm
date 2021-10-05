using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AR_Domain.SeedWork;

namespace AR_Domain.AggregateModel.AppointmentAggregate
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
    }
}
