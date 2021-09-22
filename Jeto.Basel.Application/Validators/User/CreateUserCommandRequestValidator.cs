using FluentValidation;
using Jeto.Basel.Domain.Messages.Requests.Commands.User;

namespace Jeto.Basel.Application.Validators.User
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommandRequest>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Surname).NotEmpty();
            RuleFor(c => c.Email).NotEmpty();
            RuleFor(c => c.Username).NotEmpty();
            RuleFor(c => c.Password).NotEmpty();
        }
    }
}