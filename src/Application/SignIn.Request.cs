namespace Api.Application;
/// <summary>
/// Request model for sign-in.
/// </summary>
/// <param name="Username">The username of the user.</param>
/// <param name="Password">The password of the user.</param>
public partial class SignIn
{
    public record Request(string Username, string Password);
}