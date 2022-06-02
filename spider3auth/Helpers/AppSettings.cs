namespace spider3auth.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int RefreshTokenTTL { get; set; }
        public int JwtTokenTTL { get; set; }
    }
}
