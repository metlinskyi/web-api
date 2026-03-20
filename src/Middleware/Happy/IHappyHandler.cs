namespace Api.Middleware.Handlers;

public interface IHappyHandler
{
 
}

/// <summary>
/// A handler interface with input and output types.
/// </summary>
public interface IHappyHandler<in TRequest, TResponse> : IHappyHandler
{
    public Task<TResponse> Execute(TRequest request, CancellationToken cancellationToken);
}
/// <summary>
/// A handler interface with only input type.
/// </summary>
public interface IHappyHandler<in TRequest> : IHappyHandler
{
    public Task Execute(TRequest request, CancellationToken cancellationToken);
}
