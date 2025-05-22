using Billiard.DTO;
using Billiard.Repositories.Account;

namespace Billiard.Services.Account
{
    public interface IAccountService
    {
        public  Task Register(RegisterModel model);


    }
}
