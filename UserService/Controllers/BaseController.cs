#region Imports
using Microsoft.AspNetCore.Mvc;
#endregion

namespace UserService.Controllers
{
    public class BaseController : Controller
    {
        #region Token Claims Methods
        protected string GetUserLogin()
        {
            try
            {
                var userLogin = User?.FindFirst("UserLogin")?.Value;
                return string.IsNullOrEmpty(userLogin) ? string.Empty : userLogin;
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion
    }
}
