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
        public Guid CustomerAccountId { get; private set; }
        public string CustomerName { get; private set; }
        public int AppointmentState { get; private set; }
        public Guid? StaffAccountId { get; private set; }
        public string? ZoomLink { get; private set; }

        Appointment()
        {

        }

        public Appointment(Guid appointmentId, int agencyId, DateTime appointmentDate, int appointmentSlotId, Guid customerAccountId, int appointmentState, string customerName)
        {
            AppointmentId = appointmentId;
            AgencyId = agencyId;
            AppointmentDate = appointmentDate;
            AppointmentSlotId = appointmentSlotId;
            CustomerAccountId = customerAccountId;
            AppointmentState = appointmentState;
            CustomerName = customerName;
        }

        public Appointment SetStaffAccountIdOnceStaffConfirmAppointment(Guid staffAccountId)
        {
            StaffAccountId = staffAccountId;

            AppointmentState = 2;
            return this;
        }

        public Appointment EditAppointmentDateAndSlot(DateTime date, int slot)
        {
            AppointmentDate = date;
            AppointmentSlotId = slot;

            AppointmentState = 1;
            ZoomLink = null;
            return this;
        }

        public Appointment SetZoomLink (string zoomLink)
        {
            ZoomLink = zoomLink;

            AppointmentState = 3;
            return this;
        }

        public Appointment CompleteAppointment()
        {
            AppointmentState = 4;
            return this;
        }
        public Appointment NoShowAppointment()
        {
            AppointmentState = 5;
            return this;
        }
    }
}
