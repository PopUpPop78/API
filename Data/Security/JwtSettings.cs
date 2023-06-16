namespace Data.Security
{
    public class JwtSettings
    {
        public string ExpenseApiAudience { get; set; }
        public string ExpenseApiIssuer { get; set; }
        public int DurationInMinutes { get; set; }
        public string JwtSymmetricKey { get; set; }
    }
}
