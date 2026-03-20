using Api.Middleware.Handlers;

namespace Api.Middleware.Routes;
/// <summary>
/// Provides extension methods for adding and mapping the mediator to the service collection and application.
/// </summary>
public static class MediatorMapping
{
    /// <summary>
    /// Adds the mediator and related services to the service collection.
    /// </summary>   
    public static void AddHappyEndpoins(this IServiceCollection services, Action<IMediatorOptions>? configure = null)    
    {
        // Create a default options instance
        var options = new MediatorOptions()
        {

        };

        if (configure != null)
            configure(options);

        // Add the assemblies from options
        var contexts = options.Endpoints.Values.Select(e => e.JsonSerializerContext).ToList();

        // Configure JSON options   
        services.ConfigureHttpJsonOptions(_ =>
        {
            _.SerializerOptions.TypeInfoResolverChain.Insert(0, MediatorSerializerContext.Default);
            contexts.ForEach(context => _.SerializerOptions.TypeInfoResolverChain.Insert(0, context));
        });

        // Create the mediator instance
        var mediator = new Mediator(
            services.BuildServiceProvider().GetRequiredService<ILogger<IMediator>>(),
            options
        );

        // Register the mediator and related services
        services.AddSingleton<IMediator>(mediator);
        services.AddSingleton<IEndpointHandlerFactory, EndpointHandlerFactory>();
        services.AddTransient<IEndpointMapper, SendEndpoint>();
        services.AddTransient<IEndpointMapper, SchemaEndpoint>();

        mediator.AddHandlers(handlerType => services.AddTransient(handlerType));
        mediator.BuildSchema();
    }
    /// <summary>
    /// Maps the mediator endpoints to the application.
    /// </summary>
    public static void MapMediator(this WebApplication app, RouteGroupBuilder group)
    {
        foreach (var map in app.Services.GetServices<IEndpointMapper>())
        {
            map.Map(group);
        }
    }
}

