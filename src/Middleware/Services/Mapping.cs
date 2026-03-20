using System.Reflection;
using Api.Application;

namespace Api.Middleware.Services;

public static class Mapping
{
    public static void MapGrpcServices(this WebApplication app, params Assembly[] assemblies)
    {
        app.MapGrpcService<SettingsService>();
    }
}