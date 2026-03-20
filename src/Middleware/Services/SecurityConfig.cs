using System.Text;

public class SecurityConfig : ISecurityConfig
{
    private readonly IConfiguration _configuration;

    public SecurityConfig(IConfiguration configuration)
    {
        _configuration = configuration;
        JwtKey = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
    }

    public byte[] JwtKey { get; private set; }
}