#region Imports
using AuthService.Interfaces;
using AuthService.Models.AppSettingsModels;
using System.Text;
using System.Text.Json;
using UtilityLibrary.Models.NotificationModels;
#endregion

namespace AuthService.Services
{
    public class NotificationService : INotificationService
    {
        #region Properties
        private readonly HttpClient _httpClient;
        private readonly NotificationUrls _notificationUrls;
        #endregion

        #region Constructors
        public NotificationService(HttpClient httpClient, NotificationUrls notificationUrls) 
        {
            _httpClient = httpClient;
            _notificationUrls = notificationUrls;
        }
        #endregion

        #region User Notification Methods
        public async Task SendUserLoginNotificationAsync(UserNotificationModel notification)
        {
            try
            {
                await _httpClient.PostAsync(_notificationUrls.LoginNotificationUrl,
                                    new StringContent(JsonSerializer.Serialize(notification), Encoding.UTF8, "application/json"));
            }
            catch
            {
            }
        }

        public async Task SendUserRegisterNotificationAsync(UserNotificationModel notification)
        {
            try
            {
                await _httpClient.PostAsync(_notificationUrls.RegisterNotificationUrl,
                                    new StringContent(JsonSerializer.Serialize(notification), Encoding.UTF8, "application/json"));
            }
            catch
            {
            }
        }
        #endregion
    }
}
