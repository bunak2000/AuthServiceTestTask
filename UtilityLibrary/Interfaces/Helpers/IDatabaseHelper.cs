#region Imports
using UtilityLibrary.Interfaces.Helpers.DatabaseRepositories;
#endregion

namespace UtilityLibrary.Interfaces.Helpers
{
    public interface IDatabaseHelper
    {
        #region Repositories
        IUsersRepository Users { get; }
        #endregion

        #region Service Methods
        Task SaveAsync();
        #endregion
    }
}
