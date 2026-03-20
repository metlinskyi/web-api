using System.Text.Json.Serialization;

namespace Api.Middleware.Handlers;
/// <summary>
/// A marker interface for handlers.
/// </summary>
public interface IHappyEndpoint
{
    public Type RequestType { get; }
    public Type ResponseType { get; }
    public Type HandlerType { get; }
    public Delegate HandlerDelegate { get; }
    public JsonSerializerContext JsonSerializerContext { get; }
}
/// <summary>
/// A handler interface with input and output types.
/// </summary>
public interface IHappyEndpoint<in TRequest, TResponse> : IHappyEndpoint
{
    public Task<TResponse> Execute(TRequest request, CancellationToken cancellationToken);
}
/// <summary>
/// A handler interface with only input type.
/// </summary>
public interface IHappyEndpoint<in TRequest> : IHappyEndpoint
{
    public Task Execute(TRequest request, CancellationToken cancellationToken);
}
