using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AR_Domain.SeedWork;
using AR_Domain.DomainEvents;
using Microsoft.OData.Edm;

namespace AR_Domain.AggregateModel.AppointmentAggregate
{
    public class Appointment : Entity, IAggregateRoot
    {
        public Guid AppointmentId { get; private set; }
        public int AgencyId { get; private set; }
        public int AppointmentType { get; private set; }
        public int AppointmentState { get; private set; }
        public DateTime AppointmentDate { get; private set; }
        public int AppointmentSlotId { get; private set; }
        public Guid UserAccountId { get; private set; }
        public Guid? StaffAccountID { get; private set; }


        Appointment()
        {

        }

        public Appointment(int agencyId, int appointmentType, int appointmentState, Date appointmentDate, int appointmentSlotId, Guid userAccountId)
        {
            AppointmentId = Guid.NewGuid();
            AgencyId = agencyId;
            AppointmentType = appointmentType;
            AppointmentState = appointmentState;
            AppointmentDate = appointmentDate;
            AppointmentSlotId = appointmentSlotId;
            UserAccountId = userAccountId;
        }
    }
}
