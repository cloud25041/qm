using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Staff_Domain.AggregateModel;
using MediatR;
using Staff_Domain.AggregateModel.AccountAggregate;

namespace Staff_Application.Commands
{
    public class CreateAccountCommand : IRequest<bool>
    {
       

        
        public CreateAccountCommand(string username, Guid accountId, string name, string password, string email, int mobile, int agencyId)
        {
            AccountId = accountId;
            Username = username;
            Name = name;
            Password = password;
            Email = email;
            Mobile = mobile;
            AgencyId = agencyId;
           Schedule = null;
        }

        public Guid AccountId { get; private set; }
        public string Username { get; private set; }
        public string Name { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public int Mobile { get; private set; }
        public int AgencyId { get; private set; }
        public Schedule Schedule { get; private set; }


    }
}
