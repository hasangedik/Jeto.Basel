using Jeto.Basel.Domain.Messages.Responses.Commands.User;

namespace Jeto.Basel.Domain.Messages.Requests.Commands.User
{
    public record CreateUserCommandRequest(string Name, string Surname, string Email, string Password, string Username) : CommandBase<CreateUserCommandResponse>;
}