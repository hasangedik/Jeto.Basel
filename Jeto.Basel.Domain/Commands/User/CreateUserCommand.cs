namespace Jeto.Basel.Domain.Commands.User
{
    public record CreateUserCommand(string Name, string Surname) : CommandBase<Contracts.UserContract>;
}