namespace Api.Extensions;

public static class AppSettings
{
    public static string Secret { get; set; } = "Secret";
    public static int Expires { get; set; } = 12;
    public static string Issuer { get; set; } = "Issuer";
    public static string Audience { get; set; } = "Audience";
    public static int RefreshTokenExpiration { get; set; } = 24;
}
