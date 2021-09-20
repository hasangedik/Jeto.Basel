using Jeto.Basel.Domain.Messages.Responses.Commands.Auth;

namespace Jeto.Basel.Domain.Messages.Requests.Commands.Auth
{
    public record LoginCommandRequest(string Username, string Password) : CommandBase<LoginCommandResponse>;
}