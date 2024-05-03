#region Imports
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Interfaces;
using UserService.Models.UserInfoModels;
#endregion

namespace UserService.Controllers
{
    [ApiController]
    [Authorize]
    public class UserInfoController : BaseController
    {
        #region Properties
        private readonly IUserInfoService _userInfoService;
        #endregion

        #region Constructors
        public UserInfoController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }
        #endregion

        #region Controllers
        [HttpGet]
        [Route("api/user/details")]
        public async Task<UserInfoResponseModel> GetTokenAsync()
        {
            var response = await _userInfoService.GetUserInfoAsync(GetUserLogin());
            Response.StatusCode = (int)response.HttpStatusCode;
            return response;
        }
        #endregion
    }
}
