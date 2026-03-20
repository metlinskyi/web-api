using Api.Middleware.Handlers;

namespace Api.Middleware.Routes;

public record SendEndpointResponse(Guid Id);

public class SendEndpoint(IMediator mediator, IEndpointHandlerFactory handlers) : IEndpointMapper
{
    public IEndpointConventionBuilder Map(RouteGroupBuilder route)
    {
        RequestDelegate requestDelegate = async context =>
        {
            var type = context.Request.Headers[nameof(IHandler.RequestType)].ToString();
            if (string.IsNullOrEmpty(type))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("RequestType header is required.");
                return;
            }

            var handlerInfo = handlers.GetHandlerInfo(context.RequestServices, type);
            if (handlerInfo == null)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync($"No handler found for type '{type}'.");
                return;
            }

            if (handlerInfo.Handler is IHttpContext httpContextHandler)
            {
                httpContextHandler.HttpContext = context;
            }

            if (handlerInfo.IsAuthorized(context.User) == false)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("You are not authorized to access this endpoint.");
                return;
            }

            var payload = await context.Request.ReadFromJsonAsync(handlerInfo.RequestType, mediator.Contexts.First());
            object? response = await handlerInfo.Handle(payload);
            if (response == null)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync($"An error occurred while processing the '{type}' request.");
                return;
            }

            context.Response.StatusCode = StatusCodes.Status200OK;
            await context.Response.WriteAsJsonAsync(response, handlerInfo.ResponseType, mediator.Contexts.First());
        };
        
        return route.MapPost("mediator.send", requestDelegate);       
    }
}