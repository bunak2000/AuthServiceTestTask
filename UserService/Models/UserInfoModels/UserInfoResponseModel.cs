#region Imports
using UtilityLibrary.Models;
#endregion

namespace UserService.Models.UserInfoModels
{
    public class UserInfoResponseModel : BaseResponseModel
    {
        public string Login { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public DateTime RegisterDate { get; set; } = DateTime.MinValue;
    }
}
