using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Staff_Domain.AggregateModel.AccountAggregate;
using Staff_Domain.AggregateModel.AgencyAggregate;
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
                Account account = new Account(Guid.NewGuid(), "testName","testUsername", "testPassword", "testEmail", 12345678,1);
                Agency agency = new Agency(1, "A123B123", "HDB","sports" ,5, 5);
                // Appointment appointment = new Appointment(Guid.NewGuid(), Guid.NewGuid(), 999, "testAgencyCode", DateTime.Now, DateTime.Now);
                staffContext.Add(account);
                staffContext.Add(agency);
                //staffContext.Add(appointment);
                staffContext.SaveChanges();
            }
        }
    }
}
