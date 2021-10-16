﻿using System;
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
    public class AgencyController: Controller
    {
        private readonly IMediator _mediator;
        private AgencyQueries _agencyQueries;

        public AgencyController(IMediator mediator, AgencyQueries agencyQueries)
        {
            _mediator = mediator;
            _agencyQueries = agencyQueries;
        }


        [Route("api/agency/CheckAgencyPinValid")]
        [HttpPost]
        public async Task<AgencyPinClass> CheckAgencyPinValid(SignUpAccountDetails signUpAccountDetails)
        {
            AgencyPinClass agencyPin = new AgencyPinClass();
            var isValidFlag = await _agencyQueries.CheckAgencyPinValid(signUpAccountDetails.StaffKey);
            agencyPin.AgencyIsValidFlag = isValidFlag;
            agencyPin.AgencyPinNumber = 12345;

            return agencyPin;
        }


       /* [Route("api/agency/CheckAgencyPin")]
        [HttpPost]
        public async Task<AgencyPinClass> CheckAgencyPin(SignUpAccountDetails signUpAccountDetails)
        {
            bool isAgencyPinValid = 

            AgencyViewModel agency = await _agencyQueries.GetAgencybyId(signUpAccountDetails.StaffKey);
            try
            {

                // do a query using agencypin on agency table 

                // return agencyId
                int agencyId = 1;
                agencyPinClass.AgencyPinNumber = agencyId;
                return agencyPinClass;
            }
            catch (Exception ex)
            {
                return agencyPinClass;
            }


        }
       */

    }
}