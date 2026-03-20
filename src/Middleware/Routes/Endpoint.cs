namespace Api.Middleware.Routes;

public interface IEndpointMapper    
{
    public IEndpointConventionBuilder Map(RouteGroupBuilder route);
}

public interface IEndpointHandlerFactory
{
    IEndpointHandlerInfo? GetHandlerInfo(IServiceProvider serviceProvider, string type);
}