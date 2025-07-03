using Billiard.DTO;
using Billiard.Repositories.Account;

namespace Billiard.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accRepo;
        public AccountService(AccountRepository accRepo)
        {
            _accRepo = accRepo;
        }

        public async Task<bool> changeStatusAccount(int accountId)
        {
           return await _accRepo.changeStatusAccount(accountId);
        }

      

        public async Task<IEnumerable<Models.Account>> GetAccounts()
        {
            return await _accRepo.GetAccounts();
        }

        public async Task<bool> Register(RegisterModel model)
        {
            return await this._accRepo.Register(model);
        }

        public async Task<string> resetPassword(int accountId)
        {
            return await _accRepo.resetPassword(accountId);
        }


    }
}
