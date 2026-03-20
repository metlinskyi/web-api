using System.Text.Json.Serialization;
using Api.Middleware.Handlers;

namespace Api.Application;
/// <summary>
/// Service to handle user sign-in and JWT token generation.
/// </summary>
public partial class SignIn : IHappyEndpoint<SignIn.Request, SignIn.Response>
{
    [
        JsonSerializable(typeof(Request)),
        JsonSerializable(typeof(Response)),
    ]
    private partial class SignInContext : JsonSerializerContext {}
    public Type RequestType => typeof(Request);
    public Type ResponseType => typeof(Response);
    public Delegate HandlerDelegate => Execute;
    public JsonSerializerContext JsonSerializerContext => SignInContext.Default;
    public Type HandlerType => typeof(Handler);
    public Task<Response> Execute(Request request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Add(IServiceCollection services)
    {
        throw new NotImplementedException();
    }
}