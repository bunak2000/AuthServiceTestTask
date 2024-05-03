#region Imports
using AuthService.Interfaces;
using UtilityLibrary;
using AuthService.Services;
using UtilityLibrary.Models.JwtModels;
using AuthService.Models.AppSettingsModels;
#endregion

var builder = WebApplication.CreateBuilder(args);

#region Services
var jwtOptions = builder.Configuration
    .GetSection("AuthOptions")
    .Get<JwtOptions>();

var notificationUrls = builder.Configuration
    .GetSection("NotificationUrls")
    .Get<NotificationUrls>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

UsersDatabaseDependencyInitializer.InjectUsersDatabase(builder.Services, true);

builder.Services.AddSingleton(jwtOptions);
builder.Services.AddSingleton(notificationUrls);
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddHttpClient<INotificationService, NotificationService>();
builder.Services.AddTransient<INotificationService, NotificationService>();
#endregion

#region App
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion