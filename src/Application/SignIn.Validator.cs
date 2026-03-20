using Api.Middleware.Handlers;

namespace Api.Application;
/// <summary>
/// Validator for sign-in request.
/// </summary>
public partial class SignIn
{
    public class Validator(ILogger<SignIn> logger) : IHappyValidator<Request>
    {
        public bool IsValid(Request request, out IEnumerable<string> errors)
        {
            logger.LogInformation("Validating sign-in request for user: {Username}", request.Username);
            errors = Enumerable.Empty<string>();
            return true;
        }
    }
}
