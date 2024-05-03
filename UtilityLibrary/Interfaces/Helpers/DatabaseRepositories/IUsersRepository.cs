#region Imports
using UtilityLibrary.Models.DatabaseModels;
#endregion

namespace UtilityLibrary.Interfaces.Helpers.DatabaseRepositories
{
    public interface IUsersRepository : IRepositoryBase<UserModel>
    {
        #region Get Methods
        Task<UserModel?> GetUserByLoginAsync(string login);
        #endregion
    }
}
