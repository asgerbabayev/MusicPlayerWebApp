using Bussines.Abstract;
using Bussines.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Bussines;

public static class ServiceConfiguration
{
    public static IServiceCollection AddBussines(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<ISongService, SongManager>();
        return services;
    }
}