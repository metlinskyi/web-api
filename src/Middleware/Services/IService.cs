namespace Api.Middleware.Services;
/// <summary>
/// A marker interface for services.
/// </summary>
public interface IService
{
    
}

public interface IService<TInput, TOutput> : IService
{
    Task<TOutput> Execute(TInput input, CancellationToken cancellationToken);
}

