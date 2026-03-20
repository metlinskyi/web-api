using System.Text.Json.Serialization;
using Api.Middleware.Handlers;
using Microsoft.AspNetCore.Authorization;

namespace Api.Application;
/// <summary>
/// Service to handle user sign-out and JWT token invalidation.
/// </summary>
[Authorize]
public partial class SignOut : IHappyEndpoint<SignOut.Request>    
{
    [
        JsonSerializable(typeof(Request)),
    ]
    private partial class SignOutContext : JsonSerializerContext {}
    public Type RequestType => typeof(Request);
    public Type ResponseType => typeof(void);
    public Delegate HandlerDelegate => Execute;
    public JsonSerializerContext JsonSerializerContext => SignOutContext.Default;
    public Type HandlerType => typeof(SignOut);

    public Task Execute(Request request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Add(IServiceCollection services)
    {
        throw new NotImplementedException();
    }
}