using Billiard.DTO;

namespace Billiard.Repositories.Account
{
    public interface IAccountRepository
    {
        public Task<bool> Register(RegisterModel model);

        public Task<IEnumerable<Models.Account>> GetAccounts();

        public Task<bool> changeStatusAccount(int accountId);

        public Task<string> resetPassword(int accountId);

  
    }
}
