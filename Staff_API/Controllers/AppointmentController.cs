using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Staff_Application.Commands;
using Staff_Application.Queries;
using Staff_API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Staff_API.Controllers
{
    [ApiController]
    public class AppointmentController : Controller
    {
        private readonly IMediator _mediator;
        private AppointmentQueries _appointmentQueries;

        public AppointmentController(IMediator mediator, AppointmentQueries appointmentQueries)
        {
            _mediator = mediator;
            _appointmentQueries = appointmentQueries;
        }

        [Route("api/appointment/AssignStaffIdToAppointment")]
        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> AssignStaffIdToAppointment(AcceptAppointmentDetails acceptAppointmentDetails)
        {
            return await _mediator.Send(new AssignStaffToAppointmentCommand() { StaffId = acceptAppointmentDetails.AccountId, AppointmentId = acceptAppointmentDetails.AppointmentId});
        }


        [Route("api/appointment/GetAppointmentDetailsByAgencyId")]
        [HttpPost]
        
        public async Task <List<ViewAllAppointmentViewModel>> GetAppointmentDetailsByAgencyId([FromBody] int agencyId)
        {
            List<ViewAllAppointmentViewModel> agencyList = new List<ViewAllAppointmentViewModel>();

            agencyList = await _appointmentQueries.GetAllAppointmentByAgencyId(agencyId);

            return agencyList;
        }

        [Route("api/appointment/GetAppointmentDetailsByStaffAccountId")]
        [HttpPost]
        public async Task<List<AppointmentViewModel>> GetAppointmentDetailsByStaffAccountId([FromBody] Guid accountId)
        {
            List<AppointmentViewModel> Appointment = new List<AppointmentViewModel>();

            // IList<AppointmentDetails> appointmentDetails = new List<AppointmentDetails>();
            try
            {

                Appointment = await _appointmentQueries.GetAllAppointmentsByStaffAccountId(accountId);
                return Appointment;
            }

            catch (Exception ex)
            {
                string e = ex.ToString();
                return Appointment;
            }


        }

        [Route("api/appointment/UpdateAppointmentState")]
        [HttpPost]

        public async Task<ActionResult<bool>> UpdateAppointmentState(PhysicalCounterDetails physicalCounterDetails)
        {
            if (physicalCounterDetails.state == 4)
            {
                return await _mediator.Send(new AppointmentCompleteCommand(physicalCounterDetails.appointmentId));
            }
            if (physicalCounterDetails.state == 5)
            {
                return await _mediator.Send(new NoShowAppointmentCommand(physicalCounterDetails.appointmentId));
            }
            else
            {
                return false;
            }
        }

        /*public async Task<List<ViewAppointmentDetails>> GetAppointmentDetailsByAgencyId(int agencyId)
        {
            //  List<ViewAppointmentDetails> viewAppointmentDetailsList = await _appointmentQueries.GetAllAppointmentsByAgencyId(agencyId);
            List<ViewAppointmentDetails> viewAppointmentDetailsList =  new List<ViewAppointmentDetails>();
            return viewAppointmentDetailsList;
        }
        */




        #region Agency
        //This one needs to be in Staff API
        [Route("api/appointment/getagencyinfolist")]
        [HttpGet]
        public async Task<List<AgencyViewModel>> GetAgencyInfo()
        {

            List<AgencyViewModel> AgencyList = await _appointmentQueries.GetAgencyInfo();
            // hard code
            //AgencyList.Add(new Agency() { AgencyName = "HDB", AgencyId = 1 });
            //AgencyList.Add(new Agency() { AgencyName = "MOM", AgencyId = 2 });
            //AgencyList.Add(new Agency() { AgencyName = "SPF", AgencyId = 3 });
            //AgencyList.Add(new Agency() { AgencyName = "SAF", AgencyId = 4 });

            return AgencyList;
        }
        #endregion
    }
}
