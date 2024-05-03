## Part 1: Setup application
In order to setup the application you need to insert your connection MS SQL database connection string in file "App.config" which you can find in "AuthService" folder.

File will look like this:
```sh
<?xml version="1.0" encoding="utf-8"?>
<configuration>
	<appSettings>
		<add key="DbConnectionString" value="Your connection string here"/>
	</appSettings>
</configuration>
```

It might be any empty database. Database migrations will apply automatically.

---

## Part 2: Running application
In order to run the application you need .net runtime environment beeing installed on your PC. 

To run the application you can either:
1. In visual studio select in solution properties option of running all the projects except "UtilityLibrary" then click on "Run" button.
2. Using Windows PowerShell execute following command to run each project except "UtilityLibrary" (all folders except "UtilityLibrary"):

```sh
dotnet run
```

After this you can see the console UI where you can check all functionality.

---

## Part 3: Common architecture

Application presented as a microservice solution, where each part is responsible for each own functionality. Application consists of 5 parts:
1. AuthService - main service which is responsible for providing tokens and registering users. Also this service sends notification to Notification Service after successful operations.
2. NotificationService - service which is presenting some notification handler. In current implementation displaying incoming notifications in server console.
3. UserAuthUI - UI for auth API. Basically this is a console from which you can send different requests to API and get/display data.
4. UserService - service which provides user details for authorized users. Using token claims to fetch user data from DB.
5. UtilityLibrary - basically a big library that contains DB initialization, some of commonly used models and settings. Was created for code reusing possibilities.

---
