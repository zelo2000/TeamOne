namespace GS.Domain.Models.Configuration
{
    public class JwtSettings
    {
        public string SecurityKey { get; set; }

        public string ValidIssuer { get; set; }

        public string ValidAudience { get; set; }

        public int ExpiryInMinutes { get; set; }
    }
}
