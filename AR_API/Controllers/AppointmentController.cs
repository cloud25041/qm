using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using AR_Application.Commands;
using AR_Application.Queries;
using AR_API.Models;

namespace AR_API.Controllers
{
    [ApiController]
    public class AppointmentController : Controller
    {
        IMediator _mediator;
        AppointmentQueries _appointmentQueries;

        public AppointmentController(IMediator mediator, AppointmentQueries appointmentQueries)
        {
            _mediator = mediator;
            _appointmentQueries = appointmentQueries;
        }

        [Route("api/account/getallappointments")]
        [HttpGet]
        public async Task<List<AppointmentViewModel>> GetAllAccounts()
        {
            List<AppointmentViewModel> listOfAppointment = await _appointmentQueries.GetAllAppointments();
            return listOfAppointment;
        }

        [Route("api/account/getallappointmentsbyaccountid")]
        [HttpGet]
        public async Task<List<AppointmentViewModel>> GetAllAppointmentsByAccountId(Guid accountId)
        {
            List<AppointmentViewModel> listOfAppointment = await _appointmentQueries.GetAllAppointmentsByAccountId(accountId);
            return listOfAppointment;
        }

        [Route("api/account/createAppointment")]
        [HttpPost]
        public async Task<bool> CreateAppointment(string username, string agencyCode, DateTime startTime, DateTime endTime)
        {
            return await _mediator.Send(new CreateAppointmentCommand(username, agencyCode, startTime, endTime));
        }

        [Route("api/userqueue/getagencyappointmentinformation")]
        [HttpPost]
        public IList<AvailableSlots> GetAvailableSlotsByAgencyId(UserAgencyInput userAgencyInput)
        {
            if(userAgencyInput.AgencyId == 1 && userAgencyInput.AppointmentTypeId == 1)
            {
                IList<AvailableSlots> availableSlots = new List<AvailableSlots>();
                // hard code
                availableSlots.Add(new AvailableSlots() { Date = DateTime.Now.Date, Time = "1000hrs", SlotId = 1 });
                availableSlots.Add(new AvailableSlots() { Date = DateTime.Now.Date, Time = "1100hrs", SlotId = 2 });
                availableSlots.Add(new AvailableSlots() { Date = DateTime.Now.Date, Time = "1200hrs", SlotId = 3 });
                availableSlots.Add(new AvailableSlots() { Date = new DateTime(2021, 10, 30), Time = "1100hrs", SlotId = 2 });
                availableSlots.Add(new AvailableSlots() { Date = new DateTime(2021, 01, 11), Time = "1200hrs", SlotId = 3 });
                availableSlots.Add(new AvailableSlots() { Date = new DateTime(2021, 01, 16), Time = "1400hrs", SlotId = 5 });
                availableSlots.Add(new AvailableSlots() { Date = new DateTime(2021, 01, 16), Time = "1600hrs", SlotId = 6 });
                return availableSlots;
            }
            if (userAgencyInput.AgencyId == 1 && userAgencyInput.AppointmentTypeId == 2)
            {
                IList<AvailableSlots> availableSlots = new List<AvailableSlots>();
                // hard code
                availableSlots.Add(new AvailableSlots() { Date = DateTime.Now.Date, Time = "1000hrs", SlotId = 1 });
                availableSlots.Add(new AvailableSlots() { Date = DateTime.Now.Date, Time = "1100hrs", SlotId = 2 });
                availableSlots.Add(new AvailableSlots() { Date = DateTime.Now.Date, Time = "1200hrs", SlotId = 3 });
                availableSlots.Add(new AvailableSlots() { Date = new DateTime(2021, 10, 30), Time = "0000hrs", SlotId = 0 });
                availableSlots.Add(new AvailableSlots() { Date = new DateTime(2021, 01, 11), Time = "1200hrs", SlotId = 3 });
                availableSlots.Add(new AvailableSlots() { Date = new DateTime(2021, 01, 16), Time = "1400hrs", SlotId = 5 });
                availableSlots.Add(new AvailableSlots() { Date = new DateTime(2021, 01, 16), Time = "1600hrs", SlotId = 6 });
                return availableSlots;
            }
            else
            {
                IList<AvailableSlots> availableSlots = new List<AvailableSlots>();
                // hard code
                availableSlots.Add(new AvailableSlots() { Date = DateTime.Now.Date, Time = "1000hrs", SlotId = 1 });
                availableSlots.Add(new AvailableSlots() { Date = DateTime.Now.Date, Time = "1100hrs", SlotId = 2 });
                availableSlots.Add(new AvailableSlots() { Date = DateTime.Now.Date, Time = "1200hrs", SlotId = 3 });
                availableSlots.Add(new AvailableSlots() { Date = new DateTime(2021, 10, 30), Time = "2300hrs", SlotId = 9 });
                availableSlots.Add(new AvailableSlots() { Date = new DateTime(2021, 01, 11), Time = "1200hrs", SlotId = 3 });
                availableSlots.Add(new AvailableSlots() { Date = new DateTime(2021, 01, 16), Time = "1400hrs", SlotId = 5 });
                availableSlots.Add(new AvailableSlots() { Date = new DateTime(2021, 01, 16), Time = "1600hrs", SlotId = 6 });
                return availableSlots;
            }
                
        }

        [Route("api/userqueue/getagencylist")]
        [HttpGet]
        public IList<Agency> GetAllAgency()
        {
            IList<Agency> AgencyList = new List<Agency>();
            // hard code
            AgencyList.Add(new Agency() { AgencyName = "HDB", AgencyId = 1 });
            AgencyList.Add(new Agency() { AgencyName = "MOM", AgencyId = 2 });
            AgencyList.Add(new Agency() { AgencyName = "SPF", AgencyId = 3 });
            AgencyList.Add(new Agency() { AgencyName = "SAF", AgencyId = 4 });
            
            return AgencyList;
        }


        [Route("api/account/GetAppointmentByAppointmentId")]
        [HttpPost]
        public async Task<AppointmentViewModel> GetAppointmentByAppointmentId(Guid appointmentId)
        {
            AppointmentViewModel Appointment = new AppointmentViewModel();
            Appointment = await _appointmentQueries.GetAppointmentByAppointmentId(appointmentId);
            return Appointment;
        }
    }
}
