

using Microsoft.AspNetCore.Mvc;

public class ApiRouteAttribute : RouteAttribute
{
    public static string Prefix {get;} = "api";
    public ApiRouteAttribute() : base($"{Prefix}/[controller]")
    {
    }
}