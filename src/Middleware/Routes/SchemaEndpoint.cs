namespace Api.Middleware.Routes;

public record SchemaEndpointResponse(Dictionary<string, Dictionary<string, string>> Schema);

public class SchemaEndpoint(IMediator mediator) : IEndpointMapper
{
    public IEndpointConventionBuilder Map(RouteGroupBuilder route)
    {
        mediator.Logger.LogInformation("Schema endpoint called, returning schema with {Count} types", mediator.Schema.Count);

        RequestDelegate requestDelegate = async context =>
        {
            await context.Response.WriteAsJsonAsync(
                new SchemaEndpointResponse(mediator.Schema), 
                typeof(SchemaEndpointResponse), 
                MediatorSerializerContext.Default);
        };
        
        return route.MapGet("mediator.schema", requestDelegate);       
    }
}
