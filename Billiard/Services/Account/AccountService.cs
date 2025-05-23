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
        public async Task<bool> Register(RegisterModel model)
        {
            return await this._accRepo.Register(model);
        }
    }
}
