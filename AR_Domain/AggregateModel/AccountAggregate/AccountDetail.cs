using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AR_Domain.AggregateModel.AccountAggregate
{
    public record AccountDetail(string Name, string Email, int Mobile);
}
