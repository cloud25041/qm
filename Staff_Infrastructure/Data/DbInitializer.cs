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
                Agency agency = new Agency(1, "A123B123", "HDB","Sport Hall Level 1" ,5, 5);
                Agency agency2 = new Agency(2, "B123C123", "CPF", "Public Service Center", 5, 5);
                Agency agency3 = new Agency(3, "C123D123", "MSF", "Public Service Center Level 2", 5, 5);
                // Appointment appointment = new Appointment(Guid.NewGuid(), Guid.NewGuid(), 999, "testAgencyCode", DateTime.Now, DateTime.Now);
                staffContext.Add(account);
                staffContext.Add(agency);
                staffContext.Add(agency2);
                staffContext.Add(agency3);
                //staffContext.Add(appointment);
                staffContext.SaveChanges();
            }
        }
    }
}
