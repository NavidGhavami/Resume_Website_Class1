using Microsoft.EntityFrameworkCore;
using Resume.Domain.Context;
using Resume.Domain.Entities.Common;

namespace Resume.Domain.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {

        private readonly DatabaseContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetQuery()
        {
            return _dbSet.AsQueryable();
        }

        public async Task AddEntity(TEntity entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.UpdateDate = DateTime.Now;
            await _dbSet.AddAsync(entity);
        }

        public async Task<TEntity> GetEntityById(long id)
        {
            return await _dbSet.SingleOrDefaultAsync(x => x.Id == id); ;
        }

        public void EditEntity(TEntity entity)
        {
            entity.UpdateDate = DateTime.Now;
            _dbSet.Update(entity);
        }

        public void DeleteEntity(TEntity entity)
        {
            entity.IsDelete = false;
            EditEntity(entity);
        }

        public async Task DeleteEntityById(long id)
        {
            var entity = await GetEntityById(id);
            if (entity != null) DeleteEntity(entity);
        }

        public void DeletePermanent(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }


        #region Dispose

        public async ValueTask DisposeAsync()
        {
            if (_context != null)
            {
                _context.DisposeAsync();
            }
        }

        #endregion

    }
}
