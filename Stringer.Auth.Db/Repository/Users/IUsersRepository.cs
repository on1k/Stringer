using Stringer.Auth.Domain.Models;

namespace Stringer.Auth.Db.Repository.Users;

public interface IUsersRepository
{
    Task<bool> Login(string login, string password);
    Task<IEnumerable<User?>> GetUsers();
    Task<User?> GetUserById(string id);
    Task<User?> GetUser(string login, string password);
    Task<User> CreateUser(string login, string password);
    Task UpdateUser(User user);
    Task DeleteUser(string id);
}