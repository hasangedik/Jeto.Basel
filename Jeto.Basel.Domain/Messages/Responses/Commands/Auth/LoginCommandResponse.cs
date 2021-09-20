using System;

namespace Jeto.Basel.Domain.Messages.Responses.Commands.Auth
{
    [Serializable]
    public record LoginCommandResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int? ExpiresIn { get; set; }
        public string TokenType { get; set; }
        public DateTime RefreshTokenExpireDate { get; set; }
    }
}