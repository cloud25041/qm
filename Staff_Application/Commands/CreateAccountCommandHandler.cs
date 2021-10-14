using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Staff_Domain.AggregateModel.AccountAggregate;
using Staff_Domain.SeedWork;
using MediatR;

namespace Staff_Application.Commands
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, bool>
    {
        private readonly IAccountRepository _accountRepository;
        public CreateAccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<bool> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var account = new Account(request.Username, request.Password, request.Name, request.Email, request.MobileNo);
            _accountRepository.Add(account);
            return await _accountRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
