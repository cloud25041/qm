using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AR_Domain.AggregateModel;
using MediatR;

namespace AR_Application.Commands
{
    public class CreateAccountCommand : IRequest<bool>
    {
        public CreateAccountCommand(string username, string password, string name, string email, int mobileNo)
        {
            Username = username;
            Password = password;
            Name = name;
            Email = email;
            MobileNo = mobileNo;
        }

        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public int MobileNo { get; private set; }

    }
}
