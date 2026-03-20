
using System.Text.Json.Serialization;
using Api.Middleware.Handlers;

namespace Api.Application;

public record HappyRequest();
public record HappyResponse();

public class HappyHandler(ILogger<HappyHandler> logger) : IHappyHandler<HappyRequest, HappyResponse>
{
    public Task<HappyResponse> Execute(HappyRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Executing HappyHandler with request: {Request}", request);
        throw new NotImplementedException();
    }
}

public class HappyValidator(ILogger<HappyValidator> logger) : IHappyValidator<HappyRequest>
{
    public bool IsValid(HappyRequest request, out IEnumerable<string> errors)
    {
        logger.LogInformation("Validating request: {Request}", request);
        throw new NotImplementedException();
    }
}

public partial class HappyEndpoint() :
    Happy.Endpoint<HappyRequest, HappyResponse>
         .WithHandler<HappyHandler>
         .WithValidation<HappyValidator>
{
    [
        JsonSerializable(typeof(HappyRequest)),
        JsonSerializable(typeof(HappyResponse)),
    ]
    private partial class HappyEndpointContext : JsonSerializerContext {}
    public override JsonSerializerContext JsonSerializerContext => HappyEndpointContext.Default;
}