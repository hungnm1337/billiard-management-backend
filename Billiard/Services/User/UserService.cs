
using Billiard.Repositories.User;

namespace Billiard.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Models.User> getuserById(int userId)
        {
            return await _userRepository.getuserById(userId);
        }

        public async Task<Models.User> getuserByName(string username)
        {
           return await _userRepository.getuserByName(username);
        }

        public async Task<IEnumerable<Models.User>> getUserList(int roleId)
        {
           return await _userRepository.getUserList(roleId);
        }
    }
}
