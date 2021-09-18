using System.Threading;
using System.Threading.Tasks;
using Jeto.Basel.Data.Repositories.Abstract;
using Jeto.Basel.Domain.Commands.User;
using Jeto.Basel.Domain.Contracts;
using MediatR;

namespace Jeto.Basel.Core.Features.CommandHandlers.User
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserContract>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserContract> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.AddAsync(new Basel.Domain.Models.User
            {
                Name = request.Name,
                Surname = request.Surname
            });

            return new UserContract { Name = user.Name, Surname = user.Surname };
        }
    }
}