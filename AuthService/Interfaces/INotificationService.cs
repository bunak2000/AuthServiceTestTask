#region Imports
using UtilityLibrary.Models.NotificationModels;
#endregion

namespace AuthService.Interfaces
{
    public interface INotificationService
    {
        #region User Notifications
        Task SendUserLoginNotificationAsync(UserNotificationModel notification);
        Task SendUserRegisterNotificationAsync(UserNotificationModel notification);
        #endregion
    }
}
