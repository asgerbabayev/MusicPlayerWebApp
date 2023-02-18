using Data.Identity;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.DataContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess;

public static class ServiceConfiguration
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("development")));
        services.AddScoped<ISongRepository, SongRepository>();
        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.User.RequireUniqueEmail = false;
            //options.Lockout.MaxFailedAccessAttempts = 5;
            //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(10);
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 3;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
        }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
        return services;
    }
}
