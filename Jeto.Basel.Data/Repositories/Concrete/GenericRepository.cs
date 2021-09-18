using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Jeto.Basel.Data.Context;
using Jeto.Basel.Data.Repositories.Abstract;
using Jeto.Basel.Domain;
using Microsoft.EntityFrameworkCore;
using Npgsql.Bulk;

namespace Jeto.Basel.Data.Repositories.Concrete
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DataContext _context;
        private readonly DbSet<TEntity> _entities;

        protected GenericRepository(DataContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }
        
        public async Task<TEntity> FindFirstByAsync(Expression<Func<TEntity, bool>> predicate) => await _entities.FirstOrDefaultAsync(predicate);
        
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var entityEntry = await _entities.AddAsync(entity);
            await _context.SaveEntitiesAsync();
            return entityEntry.Entity.Id != default ? entityEntry.Entity : default;
        }
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var entityEntry = _entities.Update(entity);
            await _context.SaveEntitiesAsync();
            return entityEntry.Entity;
        }
        
        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            var entityEntry = _entities.Remove(entity);
            await _context.SaveEntitiesAsync();
            return entityEntry.Entity;
        }
        
        public async Task BulkInsertAsync(IEnumerable<TEntity> entities)
        {
            var bulkUploader = new NpgsqlBulkUploader(_context);
            await bulkUploader.InsertAsync(entities);
        }
        
        public void BulkInsert(IEnumerable<TEntity> entities)
        {
            var bulkUploader = new NpgsqlBulkUploader(_context);
            bulkUploader.Insert(entities);
        }
        
        public TEntity CreateProxy() => _entities.CreateProxy();
    }
}