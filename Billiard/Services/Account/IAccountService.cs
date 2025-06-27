using Billiard.DTO;
using Billiard.Repositories.Account;

namespace Billiard.Services.Account
{
    public interface IAccountService
    {
        public  Task<bool> Register(RegisterModel model);

        public Task<IEnumerable<Models.Account>> GetAccounts();

        public Task<bool> changeStatusAccount(int accountId);

        public Task<string> resetPassword(int accountId);

    }
}
