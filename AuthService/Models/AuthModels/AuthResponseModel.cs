#region Imports
using UtilityLibrary.Models;
#endregion

namespace AuthService.Models.AuthModels
{
    public class AuthResponseModel : BaseResponseModel
    {
        public string AccessToken { get; set; } = string.Empty;
    }
}
