using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stringer.Auth.Db;
using Stringer.Auth.Db.Repository.Users;
using Stringer.Auth.Domain.Models;

namespace Stringer.Auth.Extention;

public static class ServiceCollectionExtensions
{
    public static void AddDb<TContext>(this IServiceCollection services) where TContext : DbContext
    {
        services.AddDbContext<TContext>(o =>
        {
            o.UseInMemoryDatabase("DataDb", x => 
                x.EnableNullChecks());

        });
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUsersRepository, UsersRepository>();
    }

    public static async Task PopulateUsers(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();
        var db = serviceScope.ServiceProvider.GetRequiredService<AuthContext>();
        await db.Users.AddAsync(new User { Login = "Test_1", Password = "12345", Id = "111111"});
        await db.Users.AddAsync(new User { Login = "Test_2", Password = "12345", Id = "222222" });
        await db.Users.AddAsync(new User { Login = "Test_3", Password = "12345", Id = "333333" });
        await db.SaveChangesAsync();
    }
}