namespace Infrastructure.Authorization;

public class JwtOptions
{
    public string SecretKey { get; set; }
    public int Expires { get; set; }
}