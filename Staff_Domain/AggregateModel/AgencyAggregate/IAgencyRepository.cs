using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Staff_Domain.AggregateModel.AgencyAggregate
{
    public interface IAgencyRepository
    {
        public Task<List<Agency>> GetAllAgencyAsync();
    }
}
