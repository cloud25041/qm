using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Staff_Domain.AggregateModel.AccountAggregate;
using Staff_Infrastructure.Data;


namespace Staff_Infrastructure.Data
{
    public class DbInitializer
    {
        public static void Initialize(StaffContext staffContext)
        {
            if (staffContext.Accounts.Any())
            {
            }
            else
            {
                Account account = new Account("testUsername", "testPassword", "testName", "testEmail", 12345678);
                // Appointment appointment = new Appointment(Guid.NewGuid(), Guid.NewGuid(), 999, "testAgencyCode", DateTime.Now, DateTime.Now);
                staffContext.Add(account);
                //staffContext.Add(appointment);
                staffContext.SaveChanges();
            }
        }
    }
}
