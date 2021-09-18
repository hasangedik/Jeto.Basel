using FluentValidation;
using Jeto.Basel.Domain.Commands.User;

namespace Jeto.Basel.Core.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Surname).NotEmpty();
        }
    }
}