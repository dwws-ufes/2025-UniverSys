namespace univerSys.AxPlus.Application.Options
{
    public class TokenSettings
    {
        public string SecurityKey { get; set; }
        public int ExpiringTimeInHours { get; set; }
        public int RefreshTokenExpiringTimeInHours { get; set; }
    }
}
