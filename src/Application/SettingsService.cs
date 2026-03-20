using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Api.Application;

public class SettingsService(ILogger<SettingsService> logger) : Settings.SettingsBase
{
    public override Task<Dictionary> Get(Empty request, ServerCallContext context)
    {
        logger.LogInformation("Received request for settings");
        
        var settings = new Dictionary();
        settings.Items.Add(new DictionaryItem { Key = "Setting1", Value = "Value1" });
        settings.Items.Add(new DictionaryItem { Key = "Setting2", Value = "Value2" });

        return Task.FromResult(settings);
    }
}
