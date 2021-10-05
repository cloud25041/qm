using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AR_Domain.AggregateModel;

namespace AR_Infrastructure.Data
{
    public class DbInitializer
    {
        public static void Initialize(AccountContext context)
        {
            if (context.Accounts.Any())
            {
                return;
            }
            Account account = new Account("testUsername", "testPassword", "testName", "testEmail", 12345678);
            context.Add(account);
            context.SaveChanges();
        }
    }
}
