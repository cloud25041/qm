using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AR_Domain.AggregateModel.AppointmentAggregate;
using AR_Domain.SeedWork;

namespace AR_Infrastructure.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();
    }
}
