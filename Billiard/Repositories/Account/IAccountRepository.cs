using Billiard.DTO;

namespace Billiard.Repositories.Account
{
    public interface IAccountRepository
    {
        public Task<bool> Register(RegisterModel model);
    }
}
