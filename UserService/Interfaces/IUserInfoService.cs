#region Imports
using UserService.Models.UserInfoModels;
#endregion

namespace UserService.Interfaces
{
    public interface IUserInfoService
    {
        #region User Info Methods
        Task<UserInfoResponseModel> GetUserInfoAsync(string userLogin);
        #endregion
    }
}
