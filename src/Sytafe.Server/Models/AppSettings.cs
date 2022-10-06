namespace Sytafe.Server.Models;

public record AppSettings
{
    public JwtSettings Jwt { get; set; } = new JwtSettings();
}

public record JwtSettings
{
    public string Audience { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
}