﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AR_Domain.AggregateModel
{
    public class AppointmentDetails
    {
        public Guid AccountId { get; private set; }
        public string AccountName { get; private set; }

        public AppointmentDetails(Guid accountId, string accountName)
        {
            AccountId = accountId;
            AccountName = accountName;
        }
    }
}
