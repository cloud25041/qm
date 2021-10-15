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
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        private AccountQueries _accountQueries;

        public AccountController(IMediator mediator, AccountQueries accountQueries)
        {
            _mediator = mediator;
            _accountQueries = accountQueries;
        }

        #region All Account
        [Route("api/account/getallaccount")]
        [HttpGet]
        public async Task<List<AccountViewModel>> GetAllAccounts()
        {
            List<AccountViewModel> listOfAccounts = await _accountQueries.GetAllAccounts();
            return listOfAccounts;
        }
        #endregion

        #region Login
        [Route("api/account/GetAccountByUsernameAndVerifyPassword")]
        [HttpPost]
        [ProducesResponseType(typeof(AccountViewModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AccountViewModel>> GetAccountByUsernameAndVerifyPassword(LoginDetails loginDetails)
        {
            try
            {
                AccountViewModel account = await _accountQueries.GetAccountByUsernameAndVerifyPassword(loginDetails.Username, loginDetails.Password);
                if (account != null)
                {
                    return Ok(account);
                }
                else
                {
                    return NotFound();
                }
            }

            catch(Exception ex)
            {
                string e = ex.ToString();
                return NotFound();
            }
            
        }
        #endregion

        #region Create Account
        [Route("api/account/checkUsernameAndEmailAvail")]
        [HttpPost]
        public async Task<ValidationDetails> checkUsernameAndEmailAvail(SignUpAccountDetails signUpAccountDetails)
        {
            List<AccountViewModel> listOfAccounts = await _accountQueries.GetAllAccounts();
            ValidationDetails validationDetails = new ValidationDetails();
            validationDetails.IsEmailTaken = false;
            validationDetails.IsUsernameTaken = false;

            foreach (var account in listOfAccounts)
            {
                if (account.email == signUpAccountDetails.Email)
                {
                    validationDetails.IsEmailTaken = true;

                }

                if (account.username == signUpAccountDetails.Username)
                {
                    validationDetails.IsUsernameTaken = true;
                }
            }

            return validationDetails;
        }

        #endregion

    }
}
