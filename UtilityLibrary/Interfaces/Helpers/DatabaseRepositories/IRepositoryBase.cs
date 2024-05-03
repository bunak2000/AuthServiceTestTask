namespace UtilityLibrary.Interfaces.Helpers.DatabaseRepositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        #region Insert Methods
        Task<TEntity> InsertAsync(TEntity entity, bool saveChanges = true);
        #endregion
    }
}
