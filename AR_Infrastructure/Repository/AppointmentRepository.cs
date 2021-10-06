using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AR_Domain.AggregateModel.AppointmentAggregate;
using AR_Domain.SeedWork;
using AR_Infrastructure.Data;

namespace AR_Infrastructure.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        public AppointmentRepository(AppointmentContext appointmentContext)
        {
            _appointmentContext = appointmentContext;
        }

        private readonly AppointmentContext _appointmentContext;
        public IUnitOfWork UnitOfWork { get { return _appointmentContext; } }

        public Appointment Add(Appointment appointment)
        {
            throw new NotImplementedException();
        }
    }
}
