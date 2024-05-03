#region Imports
using AuthService.Models.AuthModels;
using AuthService.Models.RegisterModels;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using UserService.Models.UserInfoModels;
#endregion

#region UI Logic
var operation = -1;
var authServiceApi = new AuthServiceApi();
while (operation != 0) 
{
    Console.WriteLine("Please, select operation:");
    Console.WriteLine("1 - Login");
    Console.WriteLine("2 - Register");
    Console.WriteLine("0 - Exit");

    do
    {
        do
        {
            Console.Write(">>> ");
        }while (!int.TryParse(Console.ReadLine(), out operation));
    } while (operation < 0 || operation > 2);

    switch (operation)
    {
        case 1:
            await authServiceApi.Login();
            break;
        case 2:
            await authServiceApi.Register();
            break;
        case 0:
            return;
        default:
            break;
    }

    Console.WriteLine("");
}
#endregion

#region Api Class
public class AuthServiceApi 
{
    #region Properties
    private readonly HttpClient _httpClient;
    #endregion

    #region Constructors
    public AuthServiceApi() 
    {
        _httpClient = new HttpClient();
    }
    #endregion

    #region Public Methods
    public async Task Login() 
    {
        Console.WriteLine("Enter Login:");
        var login = "";
        do
        {
            Console.Write(">>> ");
            login = Console.ReadLine();
            if (login != null)
            {
                login = login.Trim();
            }
        } while (string.IsNullOrEmpty(login));

        Console.WriteLine("Enter Password:");
        var password = "";
        do
        {
            Console.Write(">>> ");
            password = Console.ReadLine();
            if (password != null)
            {
                password = password.Trim();
            }
        } while (string.IsNullOrEmpty(password));

        AuthResponseModel? loginResponseModel = null;
        int statusCode = 0;
        try
        {
            var response = await _httpClient.PostAsync("https://localhost:7232/api/auth",
                                    new StringContent(JsonSerializer.Serialize(new AuthRequestModel() { Id = login, Secret = password}), Encoding.UTF8, "application/json"));

            loginResponseModel = JsonSerializer.Deserialize<AuthResponseModel>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true } );

            statusCode = (int)response.StatusCode;
        }
        catch
        {
            Console.WriteLine("Some error occured");
            return;
        }

        if (loginResponseModel == null || statusCode == 0)
        {
            Console.WriteLine("Some error occured");
            return;
        }

        if (loginResponseModel.Errors.Count > 0)
        {
            foreach (var error in loginResponseModel.Errors)
            {
                Console.WriteLine($"Error recieved: \"{error}\" with status code {statusCode}");
            }
            return;
        }

        if (string.IsNullOrEmpty(loginResponseModel.AccessToken))
        {
            Console.WriteLine("Failed to authorize");
            return;
        }

        await GetUserData(loginResponseModel.AccessToken);
    }

    public async Task Register() 
    {
        Console.WriteLine("Enter Login:");
        var login = "";
        do
        {
            Console.Write(">>> ");
            login = Console.ReadLine();
            if (login != null)
            {
                login = login.Trim();
            }
        } while (string.IsNullOrEmpty(login));

        Console.WriteLine("Enter Password:");
        var password = "";
        do
        {
            Console.Write(">>> ");
            password = Console.ReadLine();
            if (password != null)
            {
                password = password.Trim();
            }
        } while (string.IsNullOrEmpty(password));

        Console.WriteLine("Enter some details:");
        var details = "";
        do
        {
            Console.Write(">>> ");
            details = Console.ReadLine();
            if (details != null)
            {
                details = details.Trim();
            }
        } while (details == null);

        AuthResponseModel? registerResponseModel = null;
        int statusCode = 0;
        try
        {
            var response = await _httpClient.PostAsync("https://localhost:7232/api/register",
                                    new StringContent(JsonSerializer.Serialize(new RegisterRequestModel() { Id = login, Secret = password, Details = details }), 
                                    Encoding.UTF8, "application/json"));

            registerResponseModel = JsonSerializer.Deserialize<AuthResponseModel>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            statusCode = (int)response.StatusCode;
        }
        catch
        {
            Console.WriteLine("Some error occured");
            return;
        }

        if (registerResponseModel == null || statusCode == 0)
        {
            Console.WriteLine("Some error occured");
            return;
        }

        if (registerResponseModel.Errors.Count > 0)
        {
            foreach (var error in registerResponseModel.Errors)
            {
                Console.WriteLine($"Error recieved: \"{error}\" with status code {statusCode}");
            }
            return;
        }

        if (string.IsNullOrEmpty(registerResponseModel.AccessToken))
        {
            Console.WriteLine("Failed to register");
            return;
        }

        await GetUserData(registerResponseModel.AccessToken);
    }
    #endregion

    #region Private Methods
    private async Task GetUserData(string token) 
    {
        UserInfoResponseModel? userDataResponseModel = null;
        int statusCode = 0;

        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var userDataResponse = await _httpClient.GetAsync("https://localhost:7113/api/user/details");
            userDataResponseModel = JsonSerializer.Deserialize<UserInfoResponseModel>(await userDataResponse.Content.ReadAsStringAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            statusCode = (int)userDataResponse.StatusCode;
        }
        catch
        {
            Console.WriteLine("Some error occured");
            return;
        }

        if (userDataResponseModel == null || statusCode == 0)
        {
            Console.WriteLine("Some error occured");
            return;
        }

        if (userDataResponseModel.Errors.Count > 0)
        {
            foreach (var error in userDataResponseModel.Errors)
            {
                Console.WriteLine($"Error recieved: \"{error}\" with status code {statusCode}");
            }
            return;
        }

        Console.WriteLine($"User - \"{userDataResponseModel.Login}\". Regisrered at {userDataResponseModel.RegisterDate}. Details - \"{userDataResponseModel.Details}\"");
    }
    #endregion
}
#endregion