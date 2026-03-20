namespace Api.Middleware.Handlers;
/// <summary>
/// A base handler class that implements the IHandler interface. 
/// This class can be inherited by specific handlers to provide a consistent structure for handling requests and responses.
/// </summary>
public abstract class Handler<TRequest, TResponse> : IHandler<TRequest, TResponse>
{
    public Type RequestType => typeof(TRequest);
    public Type ResponseType => typeof(TResponse);
    public Delegate HandlerDelegate => Execute;
    public abstract Task<TResponse> Execute(TRequest request, CancellationToken cancellationToken);
}   
/// <summary>
/// A base handler class for handlers that do not return a response.
/// This class can be inherited by specific handlers to provide a consistent structure for handling requests without responses
/// </summary>
public abstract class Handler<TRequest> : IHandler<TRequest>
{
    public Type RequestType => typeof(TRequest);
    public Type ResponseType => typeof(void);
    public Delegate HandlerDelegate => Execute;
    public abstract Task Execute(TRequest request, CancellationToken cancellationToken);
}