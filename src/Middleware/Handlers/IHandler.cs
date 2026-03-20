namespace Api.Middleware.Handlers;
/// <summary>
/// A marker interface for handlers.
/// </summary>
public interface IHandler
{
    public Type RequestType { get; }
    public Type ResponseType { get; }
    public Delegate HandlerDelegate { get; }
}
/// <summary>
/// A handler interface with input and output types.
/// </summary>
public interface IHandler<in TRequest, TResponse> : IHandler
{
    public Task<TResponse> Execute(TRequest request, CancellationToken cancellationToken);
}
/// <summary>
/// A handler interface with only input type.
/// </summary>
public interface IHandler<in TRequest> : IHandler
{
    public Task Execute(TRequest request, CancellationToken cancellationToken);
}
