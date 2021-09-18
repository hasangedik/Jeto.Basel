using Jeto.Basel.Common.Extensions;
using Microsoft.AspNetCore.Http;

namespace Jeto.Basel.Common.Providers
{
    public class TenantProvider : ITenantProvider
    {
        private readonly bool _isTenantIdFilterEnabled;
        private readonly IHttpContextAccessor _accessor;

        public TenantProvider(IHttpContextAccessor accessor, bool isTenantIdFilterEnabled)
        {
            _isTenantIdFilterEnabled = isTenantIdFilterEnabled;
            _accessor = accessor;
        }

        public int GetTenantId()
        {
            if (_accessor?.HttpContext != null)
                return _isTenantIdFilterEnabled ? _accessor.HttpContext.User.Identity.GetTenantId() : default;

            return default;
        }

        public bool IsTenantIdFilterEnabled()
        {
            return _isTenantIdFilterEnabled;
        }
    }
}