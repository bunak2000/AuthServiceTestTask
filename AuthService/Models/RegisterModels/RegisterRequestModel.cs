namespace AuthService.Models.RegisterModels
{
    public class RegisterRequestModel
    {
        public string Id { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
    }
}
