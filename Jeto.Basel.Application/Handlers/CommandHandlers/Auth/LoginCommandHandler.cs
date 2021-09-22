using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Jeto.Basel.Common.Helpers;
using Jeto.Basel.Common.Resources;
using Jeto.Basel.Data.Repositories.Abstract;
using Jeto.Basel.Domain.Contracts;
using Jeto.Basel.Domain.Messages.Requests.Commands.Auth;
using Jeto.Basel.Domain.Messages.Responses.Commands.Auth;
using MediatR;

namespace Jeto.Basel.Application.Handlers.CommandHandlers.Auth
{
    public class LoginCommandHandler : IRequestHandler<LoginCommandRequest, LoginCommandResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtManager _jwtManager;

        public LoginCommandHandler(IUserRepository userRepository, IJwtManager jwtManager)
        {
            _userRepository = userRepository;
            _jwtManager = jwtManager;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            var (username, password) = request;
            var user = await _userRepository.FindFirstByAsync(x=> x.Username == username);

            if (user == null)
                throw new ValidationException(Literals.WrongCredentials);
            
            var isVerified = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (!isVerified)
                throw new ValidationException(Literals.WrongCredentials);
            
            var jwtToken = _jwtManager.GenerateToken(new JwtContract
            {
                Id = user.Id
            });
            
            return new LoginCommandResponse
            {
                AccessToken = jwtToken.AccessToken,
                ExpiresIn = jwtToken.ExpiresIn,
                RefreshToken = jwtToken.RefreshToken,
                TokenType = jwtToken.TokenType,
                RefreshTokenExpireDate = jwtToken.RefreshTokenExpireDate
            };
        }
    }
}