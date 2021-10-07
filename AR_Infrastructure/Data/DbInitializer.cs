using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AR_Domain.AggregateModel.AccountAggregate;
using AR_Domain.AggregateModel.AppointmentAggregate;

namespace AR_Infrastructure.Data
{
    public class DbInitializer
    {
        public static void Initialize(CustomerContext customerContext)
        {
            if (customerContext.Accounts.Any())
            {
            }
            else
            {
                Account account = new Account("testUsername", "testPassword", "testName", "testEmail", 12345678);
                Appointment appointment = new Appointment(Guid.NewGuid(), Guid.NewGuid(), 999, "testAgencyCode", DateTime.Now, DateTime.Now);
                customerContext.Add(account);
                customerContext.Add(appointment);
                customerContext.SaveChanges();
            }
        }
    }
}
