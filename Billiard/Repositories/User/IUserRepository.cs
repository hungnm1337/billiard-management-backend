namespace Billiard.Repositories.User
{
    public interface IUserRepository
    {
        Task<IEnumerable<Models.User>> getUserList(int role);

        Task<Models.User> getuserById(int id);

        Task<Models.User> getuserByName(string username);

    }
}
