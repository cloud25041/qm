using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Staff_Domain.AggregateModel.AgencyAggregate;
using Staff_Domain.AggregateModel.AppointmentAggregate;
using Staff_Domain.SeedWork;
using Staff_Infrastructure.Data;

namespace Staff_Infrastructure.Repository
{
    public class AgencyRepository : IAgencyRepository
    {
        public AgencyRepository(StaffContext staffContext)
        {
            _staffContext = staffContext;
        }
        private readonly StaffContext _staffContext;

        public IUnitOfWork UnitOfWork { get { return _staffContext; } }

        public Task<List<Agency>> GetAllAgencyAsync()
        {
            throw new NotImplementedException();
        }
    }
}
