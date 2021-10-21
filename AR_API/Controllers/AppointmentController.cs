using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using AR_Application.Commands;
using AR_Application.Queries;
using AR_Application.Model;
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

        //[Route("api/appointment/GetAppointmentDetails")]
        //[HttpPost]
        //public IList<AppointmentDetails> GetAppointmentDetails([FromBody] string accountId)
        //{
        //    IList<AppointmentDetails> appointmentDetails = new List<AppointmentDetails>();

        //    // dummy data
        //    string[] agencyArray = new string[3] { "HDB", "CPF", "MSF" };
        //    string[] serviceArray = new string[3] { "Service1", "Service2", "Service3" };
        //    string[] locationArray = new string[3] { "Location1", "Lovation2", "Location3" };
        //    // DateTime[] appointmentTimeArray = new DateTime[3] {"20210928 02:50:30.840252","20210928 02:50:30.840252", "20210928 02:50:30.840252" }




        //    for (int i = 0; i < 3; i++)
        //    {
        //        AppointmentDetails apptDetails = new AppointmentDetails();
        //        apptDetails.AppointmentName = i.ToString();
        //        apptDetails.AppointmentTime = DateTime.Now;
        //        apptDetails.Location = locationArray[i];
        //        apptDetails.Service = serviceArray[i];
        //        apptDetails.Agency = agencyArray[i];

        //        appointmentDetails.Add(apptDetails);
        //    }
        //    // appointmentDetails.AppointmentName = "12345678";
        //    return appointmentDetails;

        //}

        [Route("api/appointment/GetAppointmentDetailsByAccountId")]
        [HttpPost]
        public async Task<List<AppointmentViewModel>> GetAppointmentDetailsByAccountId([FromBody] Guid accountId)
        {
            List<AppointmentViewModel> Appointment = new List<AppointmentViewModel>();

            // IList<AppointmentDetails> appointmentDetails = new List<AppointmentDetails>();
            try
            {
                
                Appointment = await _appointmentQueries.GetAllAppointmentsByAccountId(accountId);
                return Appointment;
            }

            catch(Exception ex)
            {
                string e = ex.ToString();
                return Appointment;
            }
            

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
        public async Task<bool> CreateAppointment(int agencyId, int appointmentType, int appointmentState, DateTime appointmentDate, int appoinmentSlotId, Guid userAccountId)
        {
            return await _mediator.Send(new CreateAppointmentCommand(agencyId, appointmentType, appointmentState, appointmentDate, appoinmentSlotId, userAccountId));
        }

        // Eric - yo Justin. you need to add this route
        [Route("")]
        [HttpPost]
        public async Task<bool> EditAppointmentDateAndSlot(Guid appointmentId, DateTime date, int slot)
        {
            return await _mediator.Send(new EditAppointmentDateAndSlotCommand(appointmentId, date, slot));
        }

        //[Route("api/appointment/getagencyconcurrentuser")]
        //[HttpPost]
        //public async Task<int> GetAgencyConcurrentUser(int? AgencyId)
        //{
        //    return await _appointmentQueries.GetAgencyConcurrentUserByAgencyId(AgencyId);
        //}

        [Route("api/userqueue/getagencyappointmentinformation")]
        [HttpPost]
        public IList<AvailableSlots> GetAvailableSlotsByAgencyIdAndAppointmentType(UserAgencyInput userAgencyInput)
        {
            //List<AppointmentViewModel> listOfAppointment = await _appointmentQueries.GetAllAppointmentsByAgencyIdAndAppointmentType(userAgencyInput.AgencyId, userAgencyInput.AppointmentTypeId);
            //return listOfAppointment;
            if (userAgencyInput.AgencyId == 1 && userAgencyInput.AppointmentTypeId == 1)
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

        [Route("api/appointment/getavailableappointment")]
        [HttpPost]
        public async Task<List<AvailableSlots>> GetAvailableAppointment(UserAgencyInput userAgencyInput)
        {
            List<AvailableSlotsViewModel> resultList = await _appointmentQueries.GetAvailableAppointment(userAgencyInput.AgencyId, userAgencyInput.AppointmentTypeId, userAgencyInput.ConcurrentUser, userAgencyInput.SelectedDate);
            List<AvailableSlots> availableSlotsList = new List<AvailableSlots>();
            foreach (var result in resultList)
            {
                var date = result.Date;
                var time = result.Time;
                var slotId = result.SlotId;
                availableSlotsList.Add(new AvailableSlots() { Date = date, SlotId = slotId, Time = time });
            }
            return availableSlotsList;
        }

        //// This one needs to be in Staff API
        //[Route("api/appointment/getagencyinfolist")]
        //[HttpGet]
        //public async Task<List<AgencyViewModel>> GetAgencyInfo()
        //{

        //    List<AgencyViewModel> AgencyList = await _appointmentQueries.GetAgencyInfo();
        //    // hard code
        //    //AgencyList.Add(new Agency() { AgencyName = "HDB", AgencyId = 1 });
        //    //AgencyList.Add(new Agency() { AgencyName = "MOM", AgencyId = 2 });
        //    //AgencyList.Add(new Agency() { AgencyName = "SPF", AgencyId = 3 });
        //    //AgencyList.Add(new Agency() { AgencyName = "SAF", AgencyId = 4 });
            
        //    return AgencyList;
        //}


        [Route("api/account/GetAppointmentByAppointmentId")]
        [HttpPost]
        public async Task<AppointmentViewModel> GetAppointmentByAppointmentId([FromBody]Guid appointmentId)
        {
            AppointmentViewModel Appointment = new AppointmentViewModel();
            Appointment = await _appointmentQueries.GetAppointmentByAppointmentId(appointmentId);
            return Appointment;
        }
    }
}
