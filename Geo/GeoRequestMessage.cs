namespace abd.Geo;

internal abstract class GeoRequestMessage : HttpRequestMessage{
    public abstract Type ResponseType { get; }
}
internal class GeoRequestMessage<TResponse> : GeoRequestMessage{

    public override Type ResponseType => typeof(TResponse);

    public GeoRequestMessage()
    {
        Headers.Add("x-rapidapi-key", "6b9fe03257msh9ab51ee992b8494p159cb5jsn8354c2035ae4");
        Headers.Add("x-rapidapi-host", "wft-geo-db.p.rapidapi.com");
    }
}