#region Imports
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UtilityLibrary.Helpers;
using UtilityLibrary.Interfaces.Helpers;
#endregion

namespace UtilityLibrary
{
    public static class UsersDatabaseDependencyInitializer
    {
        #region Injection Methods
        public static void InjectUsersDatabase(IServiceCollection services, bool runMigration = false) 
        {
            services.AddDbContext<ApplicationContext>();
            services.AddScoped(typeof(IDatabaseHelper), typeof(DatabaseHelper));

            var context = services.BuildServiceProvider().GetService<ApplicationContext>();

            if (runMigration)
            {
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }
        }
        #endregion
    }
}
