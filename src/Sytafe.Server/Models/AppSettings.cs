namespace Sytafe.Server.Models;

public class AppSettings
{
    public JwtSettings Jwt { get; set; }
}

public class JwtSettings
{
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public string Key { get; set; }
}