using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Jeto.Basel.Common.Resources;
using Jeto.Basel.Data.Repositories.Abstract;
using Jeto.Basel.Domain.Messages.Requests.Commands.User;
using Jeto.Basel.Domain.Messages.Responses.Commands.User;
using MediatR;

namespace Jeto.Basel.Core.Handlers.CommandHandlers.User
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var dbUser = await _userRepository.FindFirstByAsync(x => x.Username == request.Username || x.Email == request.Email);

            if (dbUser != null)
                throw new ValidationException(Literals.UserAlreadyRegistered);
            
            var user = await _userRepository.AddAsync(new Basel.Domain.Models.User
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                Username = request.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            });

            return new CreateUserCommandResponse(user.Name, user.Surname);
        }
    }
}