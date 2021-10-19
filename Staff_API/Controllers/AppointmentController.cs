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
using Staff_Application.IntegrationEvents.Events;

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
        public async Task<ActionResult<bool>> AssignStaffIdToAppointment([FromBody] Guid staffId)
        {
            return await _mediator.Send(new AssignStaffToAppointmentCommand() { StaffId = staffId });
        }

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
