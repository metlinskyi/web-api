namespace Api.Middleware.Routes;

public interface IHttpContext
{
    public HttpContext? HttpContext { get; set; }
}

public class CurrentHttpContext : IHttpContext
{
    public HttpContext? HttpContext { get; set; }
}