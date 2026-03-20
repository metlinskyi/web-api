namespace Api.Middleware.Routes;

using System.Security.Claims;
using Api.Middleware.Handlers;

public interface IEndpointHandlerInfo
{
    public Type RequestType { get; }
    public Type ResponseType { get; }
    public IHandler Handler { get; }
    bool IsAuthorized(ClaimsPrincipal claimsPrincipal);
    Task<object?> Handle(object? payload);   
}
/// <summary>
/// A class that encapsulates information about an endpoint handler, including the handler itself, its request and response types, and authorization logic.
/// </summary>
internal class EndpointHandlerInfo(IHandler handler, IMediator mediator) : IEndpointHandlerInfo
{
    public Type RequestType => handler.RequestType;
    public Type ResponseType => handler.ResponseType;
    public IHandler Handler => handler;
    
    public bool IsAuthorized(ClaimsPrincipal claimsPrincipal)
    {
        return mediator.Access.TryGetValue(handler.GetType(), out var role) && role != null ? claimsPrincipal.IsInRole(role) : true;
    }

    public Task<Guid> Handle(EndpointPayload payload)
    {
        return Task.FromResult(Guid.NewGuid());
    }

    public async Task<object?> Handle(object? payload)
    {
        object? response = null;
        
        if (true)
        {
            response = await (dynamic) handler.HandlerDelegate.DynamicInvoke(payload, CancellationToken.None)!;
        }
        else
        {
            await (Task) handler.HandlerDelegate.DynamicInvoke(payload, CancellationToken.None)!;
        }

        return response;
    }
}