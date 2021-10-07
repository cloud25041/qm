using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using AR_Application.Commands;

namespace AR_API.Controllers
{
    [ApiController]
    public class AppointmentController : Controller
    {
        IMediator _mediator;
        public AppointmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("api/account/createAppointment")]
        [HttpPost]
        public async Task<bool> CreateAppointment(string username, string agencyCode, DateTime startTime, DateTime endTime)
        {
            return await _mediator.Send(new CreateAppointmentCommand(username, agencyCode, startTime, endTime));
        }
    }
}
