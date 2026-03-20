using System.Text.Json.Serialization;
namespace Api.Middleware.Routes;
[
    JsonSerializable(typeof(SchemaEndpointResponse)),
]
internal partial class MediatorSerializerContext : JsonSerializerContext
{
    
}