using System;

namespace Jeto.Basel.Domain.Messages.Responses.Commands.User
{
    [Serializable]
    public record CreateUserCommandResponse(string Name, string Surname);
}