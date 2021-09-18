using System;

namespace Jeto.Basel.Common.Options
{
    [Serializable]
    public class AuthOptions
    {
        public int AccessTokenExpireMinute { get; set; }
        public int RefreshTokenExpireMinute { get; set; }
    }
}