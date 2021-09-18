namespace Jeto.Basel.Domain.Commands.Auth
{
    public record LoginCommand(string Username, string Password) : CommandBase<Contracts.UserContract>;
}