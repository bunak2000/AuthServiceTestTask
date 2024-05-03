#region Imports
using Microsoft.EntityFrameworkCore;
using UtilityLibrary.Interfaces.Helpers.DatabaseRepositories;
using UtilityLibrary.Models.DatabaseModels;
#endregion

namespace UtilityLibrary.Helpers.DatabaseRepositories
{
    public class UsersRepository : RepositoryBase<UserModel>, IUsersRepository
    {
        #region Properties
        private readonly ApplicationContext context;
        #endregion

        #region Constructors
        public UsersRepository(ApplicationContext context) : base(context)
        {
            this.context = context;
        }
        #endregion

        #region Public Methods
        public async Task<UserModel?> GetUserByLoginAsync(string login)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Login == login);
            return user;
        }
        #endregion
    }
}
