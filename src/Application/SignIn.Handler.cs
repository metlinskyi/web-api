using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Api.Middleware.Handlers;
using Microsoft.IdentityModel.Tokens;

namespace Api.Application;
/// <summary>
/// Service to handle application settings retrieval.
/// </summary>
public partial class SignIn
{
    public class Handler(ILogger<SignIn> logger, ISecurityConfig security) : Handler<Request, Response>
    {
        public override Task<Response> Execute(Request request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Signing in user: {Username}", request.Username);

            // For demonstration purposes, we will just check if the username and password match a hardcoded value. In a real application, you would check against a database or an external authentication provider.
            if (request.Username == "TBB" && request.Password == "Pas$word123")
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, request.Username) }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(security.JwtKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Task.FromResult(new Response(tokenHandler.WriteToken(token)));
            }

            return Task.FromResult(new Response(string.Empty));
        }
    }
}