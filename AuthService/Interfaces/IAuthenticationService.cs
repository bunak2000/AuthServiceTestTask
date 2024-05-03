#region Imports
using AuthService.Models.AuthModels;
using AuthService.Models.RegisterModels;
#endregion

namespace AuthService.Interfaces
{
    public interface IAuthenticationService
    {
        #region Login Methods
        Task<AuthResponseModel> GetTokenAsync(AuthRequestModel requestModel);
        #endregion
        #region Register Methods
        Task<AuthResponseModel> CreateNewUserAsync(RegisterRequestModel requestModel);
        #endregion
    }
}
