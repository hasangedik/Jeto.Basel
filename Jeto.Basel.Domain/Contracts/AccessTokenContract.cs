using System;

namespace Jeto.Basel.Domain.Contracts
{
    [Serializable]
    public class AccessTokenContract
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int? ExpiresIn { get; set; }
        public string TokenType { get; set; }
        public DateTime RefreshTokenExpireDate { get; set; }
    }
}