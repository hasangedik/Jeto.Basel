namespace Jeto.Basel.Common.Providers
{
    public interface ITenantProvider
    {
        int GetTenantId();
        bool IsTenantIdFilterEnabled();
    }
}