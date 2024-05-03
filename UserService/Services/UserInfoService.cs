#region Imports
using System.Net;
using UserService.Interfaces;
using UserService.Models.UserInfoModels;
using UtilityLibrary.Interfaces.Helpers;
#endregion

namespace UserService.Services
{
    public class UserInfoService : IUserInfoService
    {
        #region Properties
        private readonly IDatabaseHelper _databaseHelper;
        #endregion

        #region Controller
        public UserInfoService(IDatabaseHelper databaseHelper)
        {
            _databaseHelper = databaseHelper;
        }
        #endregion

        #region Public Methods
        public async Task<UserInfoResponseModel> GetUserInfoAsync(string userLogin) 
        {
            var response = new UserInfoResponseModel();

            if (string.IsNullOrEmpty(userLogin))
            {
                response.AddErrorWithStatusCode("User not exists", HttpStatusCode.NotFound);
                return response;
            }

            try
            {
                var existingUser = await _databaseHelper.Users.GetUserByLoginAsync(userLogin);
                if (existingUser == null)
                {
                    response.AddErrorWithStatusCode("User not exists", HttpStatusCode.NotFound);
                    return response;
                }

                return new UserInfoResponseModel() 
                {
                    Login = existingUser.Login,
                    RegisterDate = existingUser.RegisterDate,
                    Details = existingUser.Details
                };
            }
            catch
            {
                response.AddErrorWithStatusCode("Database operation error", HttpStatusCode.InternalServerError);
                return response;
            }
        }
        #endregion
    }
}
