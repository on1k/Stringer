using Microsoft.EntityFrameworkCore;
using Stringer.Auth.Domain.Models;

namespace Stringer.Auth.Db.Repository.Users;

public class UsersRepository : IUsersRepository, IDisposable, IAsyncDisposable
{
    private AuthContext _context;

    public UsersRepository(AuthContext context)
    {
        _context = context;
    }

    public async Task<bool> Login(string login, string password)
    {
        var existUser = await _context.Users.FirstOrDefaultAsync(t => t.Login == login &&
                                                                      t.Password == password);
        return existUser != null;
    }

    public async Task<IEnumerable<User?>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetUserById(string id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User?> GetUser(string login, string password)
    {
        return await _context.Users.FirstOrDefaultAsync(t => t.Login == login &&
                                                             t.Password == password);
    }

    public async Task<User> CreateUser(string login, string password)
    {
        var newUser = new User { Login = login, Password = password };
        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();
        return newUser;
    }

    public async Task UpdateUser(User user)
    {
        _context.Entry<User>(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUser(string id)
    {
        var existStringEntity = await _context.Users.FindAsync(id);
        _context.Users.Remove(existStringEntity);
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}