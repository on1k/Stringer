using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stringer.Data.Db;
using Stringer.Data.Db.Repository.StringEntities;
using Stringer.Data.Domain.Models;

namespace Stringer.Data.Extentions;

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
        services.AddTransient<IStringEntityRepository, StringEntityRepository>();
    }

    public static async Task PopulateStringers(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();
        var db = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
        await db.StringEntities.AddAsync(new StringEntity { OwnerId = "111111", Text = "hdsjh!!!!kDKDK...." });
        await db.StringEntities.AddAsync(new StringEntity { OwnerId = "111111", Text = "hdsjh!!!!kDKDK....282hf!!!!" });
        await db.StringEntities.AddAsync(new StringEntity { OwnerId = "111111", Text = "hdsjh!!!!kDKDK....sdajlkl,,,...,,!" });
        await db.StringEntities.AddAsync(new StringEntity { OwnerId = "222222", Text = "HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH" });
        await db.StringEntities.AddAsync(new StringEntity { OwnerId = "222222", Text = "123" });
        await db.StringEntities.AddAsync(new StringEntity { OwnerId = "222222", Text = "F" });
        await db.StringEntities.AddAsync(new StringEntity { OwnerId = "222222", Text = "!qA" });
        await db.SaveChangesAsync();
    }
}