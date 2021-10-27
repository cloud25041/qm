using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Staff_Domain.AggregateModel.AccountAggregate;
//using Staff_Domain.AggregateModel.AppointmentAggregate;

namespace Staff_Domain.SeedWork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
