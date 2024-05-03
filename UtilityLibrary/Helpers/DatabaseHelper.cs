#region Imports
using UtilityLibrary.Helpers.DatabaseRepositories;
using UtilityLibrary.Interfaces.Helpers;
using UtilityLibrary.Interfaces.Helpers.DatabaseRepositories;
#endregion

namespace UtilityLibrary.Helpers
{
    public class DatabaseHelper : IDisposable, IDatabaseHelper
    {
        #region Properties
        private ApplicationContext context;
        private IUsersRepository usersRepository;
        private bool disposed = false;

        public IUsersRepository Users => usersRepository = usersRepository ?? new UsersRepository(context);
        #endregion

        #region Constructors
        public DatabaseHelper(ApplicationContext context)
        {
            this.context = context;
        }
        #endregion

        #region Public Methods
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Private Methods
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }

            this.disposed = true;
        }
        #endregion
    }
}
