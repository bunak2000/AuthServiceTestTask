#region Imports
using Microsoft.EntityFrameworkCore;
using UtilityLibrary.Interfaces.Helpers.DatabaseRepositories;
#endregion

namespace UtilityLibrary.Helpers.DatabaseRepositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        #region Properties
        private readonly ApplicationContext context;
        private readonly DbSet<TEntity> dbSet;
        private readonly int connectionTimeoutValue = 180;
        #endregion

        #region Constructors
        public RepositoryBase(ApplicationContext context)
        {
            this.context = context;
            this.context.Database.SetCommandTimeout(connectionTimeoutValue);
            this.dbSet = context.Set<TEntity>();
        }
        #endregion

        #region Virtual Methods
        public virtual async Task<TEntity> InsertAsync(TEntity entity, bool saveChanges = true)
        {
            await context.Set<TEntity>().AddAsync(entity);

            if (saveChanges)
            {
                await context.SaveChangesAsync();
            }

            return entity;
        }
        #endregion
    }
}
