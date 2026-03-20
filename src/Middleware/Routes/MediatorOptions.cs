using Api.Middleware.Handlers;

namespace Api.Middleware.Routes;

public class MediatorOptions() : IMediatorOptions
{
    public Dictionary<string, IHappyEndpoint> Endpoints { get; set; } = new Dictionary<string, IHappyEndpoint>();

    public void Add<TEndpoint>() where TEndpoint : IHappyEndpoint, new()
    {
        TEndpoint endpoint = new();
        Endpoints.TryAdd(typeof(TEndpoint).Name, endpoint);
    }
}
    
public interface IMediatorOptions
{
    Dictionary<string,IHappyEndpoint> Endpoints { get; set; }

    public void Add<TEndpoint>() where TEndpoint : IHappyEndpoint, new();

}