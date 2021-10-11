using AR_Application.Commands;
using AR_Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using AR_Application.Queries;
using AR_Application.Commands;
using AR_API.Models;

namespace AR_API.Controllers
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

        [Route("api/account/getallaccount")]
        [HttpGet]
        public async Task<List<AccountViewModel>> GetAllAccounts()
        {
            List<AccountViewModel> listOfAccounts = await _accountQueries.GetAllAccounts();
            return listOfAccounts;
        }

        [Route("api/account/checkUsernameAndEmailAvail")]
        [HttpPost]
        public async Task<ValidationDetails> checkUsernameAndEmailAvail(SignUpAccountDetails signUpAccountDetails)
        {
            List<AccountViewModel> listOfAccounts = await _accountQueries.GetAllAccounts();
            ValidationDetails validationDetails = new ValidationDetails();
            validationDetails.IsEmailTaken = false;
            validationDetails.IsUsernameTaken = false;

            foreach(var account in listOfAccounts)
            {
                if(account.email == signUpAccountDetails.Email)
                {
                    validationDetails.IsEmailTaken = true;
                    
                }

                if(account.username == signUpAccountDetails.Username)
                {
                    validationDetails.IsUsernameTaken = true;
                }
            }

            return validationDetails;
        }


        [Route("api/account/getaccountbyusername")]
        [HttpGet]
        public async Task<AccountViewModel> GetAccountByUsername(string username)
        {
            AccountViewModel account = await _accountQueries.GetAccountByUsername(username);
            return account;
        }

        [Route("api/account/GetAccountByUsernameAndVerifyPassword")]
        [HttpPost]
        [ProducesResponseType(typeof(AccountViewModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AccountViewModel>> GetAccountByUsernameAndVerifyPassword(LoginDetails loginDetails)
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

        [Route("api/account/createaccount")]
        [HttpPost]
        public async Task<bool> CreateAccount(SignUpAccountDetails signUpAccountDetails)
        {
            string name = signUpAccountDetails.FirstName + " " + signUpAccountDetails.LastName;
            return await _mediator.Send(new CreateAccountCommand(signUpAccountDetails.Username, signUpAccountDetails.Password, name, signUpAccountDetails.Email, signUpAccountDetails.MobileNo));
        }
    }
}
