using Bussines.Abstract;
using Bussines.Concrete;
using DataAccess.Abstract.UnitOfWork;
using DataAccess.Concrete.UnitOfWork;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using MusicPlayer.Bussines.Abstract;
using MusicPlayer.Bussines.Concrete;
using System.Reflection;

namespace Bussines;

public static class ServiceConfiguration
{
    public static IServiceCollection AddBussines(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
        services.AddScoped<ISongService, SongManager>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthService, AuthManager>();
        return services;
    }
}