using Api.Middleware.Handlers;

namespace Api.Middleware.Routes;

internal class EndpointHandlerFactory(IMediator mediator) : IEndpointHandlerFactory
{
    public IEndpointHandlerInfo? GetHandlerInfo(IServiceProvider serviceProvider, string type)
    {
        var key = type.Replace("Request", "");
        mediator.Logger.LogInformation("Looking for handler of type '{Type}' with key '{Key}'", type, key);
        mediator.Handlers.TryGetValue(key, out var handlerType);
        if (handlerType == null)  
        {
            return null;
        }
        var handler = serviceProvider.GetService(handlerType) as IHandler;
        if (handler == null)
        {
            return null;
        }
        return new EndpointHandlerInfo(handler, mediator);
    }

    public THandler GetHandler<THandler>() where THandler : IHandler
    {
        return default(THandler)!;
    }
}

