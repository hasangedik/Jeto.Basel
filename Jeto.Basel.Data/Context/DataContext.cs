using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Jeto.Basel.Common.Extensions;
using Jeto.Basel.Common.Providers;
using Jeto.Basel.Domain;
using Microsoft.EntityFrameworkCore;

namespace Jeto.Basel.Data.Context
{
    public class DataContext : DbContext
    {
        private readonly ITenantProvider _tenantProvider;
        
        public DataContext(DbContextOptions<DataContext> options, ITenantProvider tenantProvider) : base(options)
        {
            _tenantProvider = tenantProvider;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Tenant - CompanyId Filter

            if (_tenantProvider != null && _tenantProvider.IsTenantIdFilterEnabled())
            {
                var typesClient = modelBuilder.Model.GetEntityTypes()
                    .Where(e => typeof(IHasCompanyId).IsAssignableFrom(e.ClrType));

                foreach (var entityType in typesClient)
                {
                    var clientIdProperty = entityType.FindProperty("CompanyId");
                    if (clientIdProperty == null || clientIdProperty.ClrType != typeof(int))
                        continue;

                    modelBuilder.Entity(entityType.ClrType).AddQueryFilter<IHasCompanyId>(e => e.CompanyId == _tenantProvider.GetTenantId());
                }
            }

            #endregion
            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public async Task<int> SaveEntitiesAsync()
        {
            SetAuditProperties();
            return await SaveChangesAsync();
        }

        private const string CreatedOnFieldName = "CreatedOn";
        private const string UpdatedOnFieldName = "UpdatedOn";
        private const string CreatedByFieldName = "CreatedBy";
        private const string UpdatedByFieldName = "UpdatedBy";

        private void SetAuditProperties()
        {
            Guid? currentUserId = null;
            try
            {
                //INFO: get user some where implement it
            }
            catch
            {
                //User not initialized ignore
            }

            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity.HasProperty(CreatedOnFieldName))
                        entry.Entity.SetPropertyValue(CreatedOnFieldName, DateTime.UtcNow);

                    if (entry.Entity.HasProperty(CreatedByFieldName) && currentUserId.HasValue)
                        entry.Entity.SetPropertyValue(CreatedByFieldName, currentUserId.Value);
                }
                else if (entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
                {
                    if (entry.Entity.HasProperty(UpdatedOnFieldName))
                        entry.Entity.SetPropertyValue(UpdatedOnFieldName, DateTime.UtcNow);

                    if (entry.Entity.HasProperty(UpdatedByFieldName) && currentUserId.HasValue)
                        entry.Entity.SetPropertyValue(UpdatedByFieldName, currentUserId.Value);
                }
            }
        } 
    }
}