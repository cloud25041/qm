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

        [Route("api/UserQueue/GetAppointmentDetails")]
        [HttpPost]
        public IList<AppointmentDetails> GetAppointmentDetails([FromBody] string accountId)
        {
            IList<AppointmentDetails> appointmentDetails = new List<AppointmentDetails>();

            // dummy data
            string[] agencyArray = new string[3] { "HDB", "CPF", "MSF" };
            string[] serviceArray = new string[3] { "Service1", "Service2", "Service3" };
            string[] locationArray = new string[3] { "Location1", "Lovation2", "Location3" };
            // DateTime[] appointmentTimeArray = new DateTime[3] {"20210928 02:50:30.840252","20210928 02:50:30.840252", "20210928 02:50:30.840252" }




            for (int i = 0; i < 3; i++)
            {
                AppointmentDetails apptDetails = new AppointmentDetails();
                apptDetails.AppointmentName = i.ToString();
                apptDetails.AppointmentTime = DateTime.Now;
                apptDetails.Location = locationArray[i];
                apptDetails.Service = serviceArray[i];
                apptDetails.Agency = agencyArray[i];

                appointmentDetails.Add(apptDetails);
            }
            // appointmentDetails.AppointmentName = "12345678";
            return appointmentDetails;

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
