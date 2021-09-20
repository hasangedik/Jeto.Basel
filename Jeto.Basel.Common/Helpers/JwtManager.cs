using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Jeto.Basel.Common.Constants;
using Jeto.Basel.Common.Options;
using Jeto.Basel.Domain.Contracts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Jeto.Basel.Common.Helpers
{
    public interface IJwtManager
    {
        AccessTokenContract GenerateToken(IJwtContract member);
    }

    public class JwtManager : IJwtManager
    {
        private readonly AuthOptions _authOptions;

        public JwtManager(IOptions<AuthOptions> authOptions)
        {
            _authOptions = authOptions.Value;
        }

        private static readonly byte[] SymmetricKey = Convert.FromBase64String(AppConstants.SymmetricKey);

        public static TokenValidationParameters ValidationParameters => new()
        {
            RequireExpirationTime = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(SymmetricKey)
        };

        public AccessTokenContract GenerateToken(IJwtContract member)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", member.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(_authOptions.AccessTokenExpireMinute),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(SymmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtToken = (JwtSecurityToken)tokenHandler.CreateToken(tokenDescriptor);

            return new AccessTokenContract
            {
                AccessToken = jwtToken.RawData,
                ExpiresIn = jwtToken.Payload.Exp,
                TokenType = AppConstants.Jwt,
                RefreshToken = GenerateRefreshToken(),
                RefreshTokenExpireDate = DateTime.UtcNow.AddMinutes(_authOptions.RefreshTokenExpireMinute)
            };
        }

        private static string GenerateRefreshToken()
        {
            var number = new byte[32];
            using var random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}