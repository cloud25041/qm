using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace AR_Domain.DomainEvents
{
    public class AppointmentCreationStartedDomainEvent : INotification
    {
        public AppointmentCreationStartedDomainEvent(string username)
        {
            Username = username;
        }
        public string Username { get; private set; }
    }
}
