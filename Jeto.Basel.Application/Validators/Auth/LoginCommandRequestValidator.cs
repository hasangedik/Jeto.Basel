using FluentValidation;
using Jeto.Basel.Domain.Messages.Requests.Commands.Auth;

namespace Jeto.Basel.Application.Validators.Auth
{
    public class LoginCommandRequestValidator : AbstractValidator<LoginCommandRequest>
    {
        public LoginCommandRequestValidator()
        {
            RuleFor(c => c.Username).NotEmpty();
            RuleFor(c => c.Password).NotEmpty();
        }
    }
}