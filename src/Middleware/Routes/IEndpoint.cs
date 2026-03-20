namespace Api.Middleware.Routes;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder builder);
}