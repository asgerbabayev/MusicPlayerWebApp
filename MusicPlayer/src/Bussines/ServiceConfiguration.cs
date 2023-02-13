using Bussines.Abstract;
using Bussines.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace Bussines;

public static class ServiceConfiguration
{
    public static IServiceCollection AddBussines(this IServiceCollection services)
    {

        services.AddScoped<ISongService, SongManager>();
        return services;
    }
}