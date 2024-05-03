#region Imports
using AuthService.Interfaces;
using AuthService.Models.AuthModels;
using AuthService.Models.RegisterModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
#endregion

namespace AuthService.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        #region Properties
        private readonly IAuthenticationService _authService;
        #endregion

        #region Constructors
        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }
        #endregion

        #region Controllers
        [HttpPost]
        [Route("api/auth")]
        public async Task<AuthResponseModel> GetTokenAsync([FromBody] AuthRequestModel requestModel)
        {
            var response = await _authService.GetTokenAsync(requestModel);
            Response.StatusCode = (int)response.HttpStatusCode;
            return response;
        }

        [HttpPost]
        [Route("api/register")]
        public async Task<AuthResponseModel> CreateNewUserAsync([FromBody] RegisterRequestModel requestModel)
        {
            var response = await _authService.CreateNewUserAsync(requestModel);
            Response.StatusCode = (int)response.HttpStatusCode;
            return response;
        }
        #endregion
    }
}
