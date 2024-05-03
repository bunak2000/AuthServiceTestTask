#region Imports
using AuthService.Interfaces;
using AuthService.Models.AuthModels;
using AuthService.Models.RegisterModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using UtilityLibrary.Helpers;
using UtilityLibrary.Interfaces.Helpers;
using UtilityLibrary.Models.DatabaseModels;
using UtilityLibrary.Models.JwtModels;
using UtilityLibrary.Models.NotificationModels;
#endregion

namespace AuthService.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Properties
        private readonly IDatabaseHelper _databaseHelper;
        private readonly JwtOptions _jwtOptions;
        private readonly INotificationService _notificationService;

        private TimeSpan tokenLifetimeMinutes;
        private SigningCredentials issuerSigningCredentials;
        #endregion

        #region Constructors
        public AuthenticationService(IDatabaseHelper databaseHelper, JwtOptions jwtOptions, INotificationService notificationService)
        {
            _databaseHelper = databaseHelper;
            _jwtOptions = jwtOptions;

            tokenLifetimeMinutes = TimeSpan.FromMinutes(_jwtOptions.ExpirationSeconds);
            issuerSigningCredentials = new SigningCredentials(new SymmetricSecurityKey(
                                Encoding.ASCII.GetBytes(_jwtOptions.SigningKey)), SecurityAlgorithms.HmacSha256);
            _notificationService = notificationService;
        }
        #endregion

        #region Public methods
        public async Task<AuthResponseModel> GetTokenAsync(AuthRequestModel requestModel)
        {
            var response = new AuthResponseModel();

            if (requestModel == null || string.IsNullOrEmpty(requestModel.Id) || string.IsNullOrEmpty(requestModel.Secret))
            {
                response.AddErrorWithStatusCode("Login and Password is required", HttpStatusCode.BadRequest);
                return response;
            }

            if (!Regex.Match(requestModel.Id, @"^[\w]{3,}$").Success)
            {
                response.AddErrorWithStatusCode("Invalid login format", HttpStatusCode.BadRequest);
                return response;
            }

            if (!Regex.Match(requestModel.Secret, @"^[\w]{3,}$").Success)
            {
                response.AddErrorWithStatusCode("Invalid password format", HttpStatusCode.BadRequest);
                return response;
            }

            var user = await _databaseHelper.Users.GetUserByLoginAsync(requestModel.Id);
            if (user == null)
            {
                response.AddErrorWithStatusCode("User not found", HttpStatusCode.NotFound);
                return response;
            }

            if (!CheckUserPassword(requestModel.Secret, user))
            {
                response.AddErrorWithStatusCode("Password is invalid", HttpStatusCode.Forbidden);
                return response;
            }

            var claims = new List<Claim> { new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login), new Claim("UserLogin", user.Login) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            var now = DateTime.UtcNow;
            var jwtToken = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer, audience: _jwtOptions.Audience, notBefore: now,
                claims: claimsIdentity.Claims, expires: now.Add(tokenLifetimeMinutes),
                signingCredentials: issuerSigningCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            await _notificationService.SendUserLoginNotificationAsync(new UserNotificationModel() 
            {
                Login = user.Login,
                LoginDate = DateTime.UtcNow
            });

            return new AuthResponseModel() { AccessToken = encodedJwt };
        }

        public async Task<AuthResponseModel> CreateNewUserAsync(RegisterRequestModel requestModel)
        {
            var response = new AuthResponseModel();

            if (requestModel == null || string.IsNullOrEmpty(requestModel.Id) || string.IsNullOrEmpty(requestModel.Secret))
            {
                response.AddErrorWithStatusCode("Login and Password is required", HttpStatusCode.BadRequest);
                return response;
            }

            if (!Regex.Match(requestModel.Id, @"^[\w]{3,}$").Success)
            {
                response.AddErrorWithStatusCode("Invalid login format", HttpStatusCode.BadRequest);
                return response;
            }

            if (!Regex.Match(requestModel.Secret, @"^[\w]{3,}$").Success)
            {
                response.AddErrorWithStatusCode("Invalid password format", HttpStatusCode.BadRequest);
                return response;
            }

            if (!Regex.Match(requestModel.Details, @"^[\w ]*$").Success)
            {
                response.AddErrorWithStatusCode("Invalid details format", HttpStatusCode.BadRequest);
                return response;
            }

            UserModel? user = null;
            try
            {
                var existingUser = await _databaseHelper.Users.GetUserByLoginAsync(requestModel.Id);
                if (existingUser != null)
                {
                    response.AddErrorWithStatusCode("User with this login already exists", HttpStatusCode.Conflict);
                    return response;
                }

                user = await _databaseHelper.Users.InsertAsync(new UserModel()
                {
                    Login = requestModel.Id,
                    Password = HashHelper.GetSHA256(requestModel.Secret),
                    RegisterDate = DateTime.UtcNow,
                    Details = requestModel.Details.Trim()
                });

            }
            catch (Exception)
            {
                response.AddErrorWithStatusCode("Database saving error", HttpStatusCode.InternalServerError);
                return response;
            }

            if (user == null)
            {
                response.AddErrorWithStatusCode("User saving error", HttpStatusCode.InternalServerError);
                return response;
            }

            var claims = new List<Claim> { new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login), new Claim("UserLogin", user.Login) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            var now = DateTime.UtcNow;
            var jwtToken = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer, audience: _jwtOptions.Audience, notBefore: now,
                claims: claimsIdentity.Claims, expires: now.Add(tokenLifetimeMinutes),
                signingCredentials: issuerSigningCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            await _notificationService.SendUserRegisterNotificationAsync(new UserNotificationModel()
            {
                Login = user.Login,
                LoginDate = DateTime.UtcNow
            });

            return new AuthResponseModel() { AccessToken = encodedJwt };
        }
        #endregion

        #region Private mewthods
        private bool CheckUserPassword(string password, UserModel user)
        {
            if (user.Password != HashHelper.GetSHA256(password))
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
