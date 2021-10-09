﻿using AR_Application.Commands;
using AR_Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

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


        [Route("api/account/getaccountbyusername")]
        [HttpGet]
        public async Task<AccountViewModel> GetAccountByUsername(string username)
        {
            AccountViewModel account = await _accountQueries.GetAccountByUsername(username);
            return account;
        }

        [Route("api/account/GetAccountByUsernameAndVerifyPassword")]
        [HttpGet]
        [ProducesResponseType(typeof(AccountViewModel),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<AccountViewModel>> GetAccountByUsernameAndVerifyPassword(string username, string password)
        {
            AccountViewModel account = await _accountQueries.GetAccountByUsernameAndVerifyPassword(username, password);
            return Ok(account);
        }

        [Route("api/account/createaccount")]
        [HttpPost]
        public async Task<bool> CreateAccount(string username, string password, string name, string email, int mobileNo)
        {
            return await _mediator.Send(new CreateAccountCommand(username, password, name, email, mobileNo));
        }
    }
}
