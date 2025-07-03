namespace Billiard.Services.User
{
    public interface IUserService
    {
        Task<IEnumerable<Models.User>> getUserList(int roleId);

        Task<Models.User> getuserById(int userId);

        Task<Models.User> getuserByName(string username);

    }
}
