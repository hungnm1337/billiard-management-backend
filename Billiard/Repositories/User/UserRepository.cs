
using Billiard.Models;
using Microsoft.EntityFrameworkCore;

namespace Billiard.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly Prn232ProjectContext _prn232ProjectContext;

        public UserRepository(Prn232ProjectContext prn232ProjectContext)
        {
            _prn232ProjectContext = prn232ProjectContext;
        }
        public async Task<Models.User> getuserById(int userId)
        {
            return await _prn232ProjectContext.Users.FindAsync(userId);
        }

        public async Task<Models.User> getuserByName(string username)
        {
            return await _prn232ProjectContext.Users.FirstOrDefaultAsync(x => x.Account.Username.Equals(username));
        }

        public async Task<IEnumerable<Models.User>> getUserList(int roleId)
        {
            if (role == 0)
            {
                return await _prn232ProjectContext.Users.ToListAsync();

            }
            else
            {
                return await _prn232ProjectContext.Users.Where(x => x.Role.RoleId == roleId).ToListAsync();
            }
        }
    }
}
