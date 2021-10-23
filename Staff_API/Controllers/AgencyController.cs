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
        public async Task<AgencyPinClass> CheckAgencyPinValid([FromBody]string agencyPin)
        {
            AgencyPinClass agencyPinClass = new AgencyPinClass();
            var isValidFlag = await _agencyQueries.CheckAgencyPinValid(agencyPin);
            agencyPinClass.AgencyIsValidFlag = isValidFlag;
            if(agencyPinClass.AgencyIsValidFlag == true)
            {
                var agencyList = await _agencyQueries.GetAgencybyAgencyPin(agencyPin);
                foreach(var agency in agencyList)
                {
                    agencyPinClass.AgencyId = agency.AgencyId;
                }
            }
            else
            {
                agencyPinClass.AgencyId = 0;
            }
           // agencyPinClass.AgencyPinNumber = 12345;

            return agencyPinClass;
        }



      /*  [Route("api/agency/GetAllAgencyList")]
        [HttpGet]
        public async Task<AgencyPinClass> GetAllAgencyList()
        {
          

           //return agencyPinClass;
        }
      */


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
