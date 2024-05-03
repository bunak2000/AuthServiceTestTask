#region Imports
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using UtilityLibrary.Models.DatabaseModels;
#endregion

namespace UtilityLibrary.Helpers
{
    public class ApplicationContext : DbContext
    {
        #region Properties
        public DbSet<UserModel> Users { get; set; }
        public bool IsDisposed { get; set; }
        #endregion

        #region Constructors
        public ApplicationContext() : base() { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.SetCommandTimeout(1800);
        }
        #endregion

        #region Protected Methods
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.AppSettings["DbConnectionString"]);
        }
        #endregion

        #region Override methods
        public override void Dispose()
        {
            IsDisposed = true;
            base.Dispose();
        }
        #endregion
    }
}
