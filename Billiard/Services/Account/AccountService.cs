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
        public async Task Register(RegisterModel model)
        {
            await this._accRepo.Register(model);
        }
    }
}
