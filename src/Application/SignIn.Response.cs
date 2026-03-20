
namespace Api.Application;
/// <summary>
/// Response model for sign-in.
/// </summary>
/// <param name="Token">The JWT token if the sign-in was successful.</param>
public partial class SignIn
{
    public record Response(string Token);
}