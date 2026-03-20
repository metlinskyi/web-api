using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Api.Middleware.Handlers;

namespace Api.Middleware.Routes;

internal class Mediator(
    ILogger<IMediator> logger, 
    IMediatorOptions options) : IMediator
{
    public Dictionary<Type, string> Access { get; } = new ();

    public ILogger<IMediator> Logger { get; } = logger;

    public JsonSerializerContext[] Contexts { get; } = options.Endpoints.Values
                                                            .Select(e => e.JsonSerializerContext).ToArray();
    public IDictionary<string, Type> Handlers { get; } = options.Endpoints.Values
                                                            .ToDictionary(e => e.HandlerType.Name.Replace("Handler", ""), e => e.HandlerType);

    public Dictionary<string, Dictionary<string, string>> Schema { get; } = new Dictionary<string, Dictionary<string, string>>();
    public Type SchemaType => typeof(Dictionary<string, Dictionary<string, string>>);

    public void AddHandlers(Action<Type> registerHandler)
    {
        foreach (var endpoint in options.Endpoints.Values)
        {
            registerHandler(endpoint.HandlerType);
        }
    }

    public void BuildSchema()
    {
        options.Endpoints.SelectMany(kv => new[] { kv.Value.RequestType, kv.Value.ResponseType })
            .Where(t => t != typeof(void))
            .Distinct()
            .ToList()
            .ForEach(GetTypeSchema);
    }    

    private void GetTypeSchema(Type type)
    {
        JsonTypeInfo? typeInfo   = null;
        foreach (var context in Contexts)
        {
            typeInfo = context.GetTypeInfo(type);
            if (typeInfo != null)
                break;
        }

        if (typeInfo == null)
        {
            Logger.LogWarning("Type {Type} not found in any context", type.FullName);
            return;
        }

        string typeName = GetTypeName(type);

        if (Schema.ContainsKey(typeName))
            return;

        Schema.Add(typeName, typeInfo.Properties.ToDictionary(p => p.Name, p => GetTypeName(p.PropertyType)));

        foreach (var prop in typeInfo.Properties)
        {
            if (prop.PropertyType.IsPrimitive || prop.PropertyType.FullName?.StartsWith("System.") == true)
                continue;

            GetTypeSchema(prop.PropertyType);
        }         
    }

    private string GetTypeName(Type type)
    {
        return (type.FullName ?? type.Name).Replace(".", "").Replace("+", "");
    }
  
}

public interface IMediator
{
    public ILogger<IMediator> Logger { get; }
    public Dictionary<Type, string> Access { get; } 
    public IDictionary<string, Type> Handlers { get; }
    public JsonSerializerContext[] Contexts { get; }   
    public Type SchemaType { get; }
    public Dictionary<string, Dictionary<string, string>> Schema { get; }

}