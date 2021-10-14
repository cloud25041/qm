using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using AR_Application.Commands;
using AR_Application.Queries;

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
