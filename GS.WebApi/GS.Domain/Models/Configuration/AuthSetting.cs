namespace GS.Domain.Models.Configuration
{
    public class AuthSetting
    {
        public string SecretKey { get; set; }

        public int ExpiredAt { get; set; }
    }
}
