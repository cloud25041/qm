using Staff_Domain.SeedWork;
using System;

namespace Staff_Domain.AggregateModel.AppointmentAggregate
{
    public class Appointment : Entity, IAggregateRoot
    {
        public Guid AppointmentId { get; private set; }
        public int AgencyId { get; private set; }
        public DateTime AppointmentDate { get; private set; }
        public int AppointmentSlotId { get; private set; }
        public Guid UserAccountId { get; private set; }
        public Guid StaffAccountId { get; private set; }
        public int AppointmentState { get; private set; }
        public string ZoomLink { get; private set; }

        Appointment()
        {

        }

        public Appointment(Guid appointmentId, int agencyId, DateTime appointmentDate, int appointmentSlotId, Guid userAccountId, int appointmentState)
        {
            AppointmentId = appointmentId;
            AgencyId = agencyId;
            AppointmentDate = appointmentDate;
            AppointmentSlotId = appointmentSlotId;
            UserAccountId = userAccountId;
            AppointmentState = appointmentState;
        }

        public Appointment SetStaffAccountIdOnceStaffConfirmAppointment(Guid staffAccountId)
        {
            StaffAccountId = staffAccountId;
            AppointmentState = 2;
            return this;
        }

        public Appointment SetZoomLink (string zoomLink)
        {
            ZoomLink = zoomLink;
            AppointmentState = 3;
            return this;
        }
    }
}
