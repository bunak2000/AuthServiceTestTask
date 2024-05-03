#region Imports
using Microsoft.AspNetCore.Mvc;
using UtilityLibrary.Models.NotificationModels;
#endregion

namespace NotificationService.Controllers
{
    [ApiController]
    public class NotificationController : Controller
    {
        #region User Notification Controllers
        [HttpPost]
        [Route("api/notification/users/login")]
        public void RecieveUserLoginNotification([FromBody] UserNotificationModel requestModel)
        {
            if (!string.IsNullOrEmpty(requestModel.Login) && requestModel.LoginDate != DateTime.MinValue)
            {
                Console.WriteLine($"Log>>> User \"{requestModel.Login}\" logged to the system in {DateTime.UtcNow}");
            }
        }

        [HttpPost]
        [Route("api/notification/users/register")]
        public void RecieveUserRegisterNotification([FromBody] UserNotificationModel requestModel)
        {
            if (!string.IsNullOrEmpty(requestModel.Login) && requestModel.LoginDate != DateTime.MinValue)
            {
                Console.WriteLine($"Log>>> User \"{requestModel.Login}\" registered to the system in {DateTime.UtcNow}");
            }
        }
        #endregion
    }
}
