namespace WebApiServer
{
    public static class Constants
    {
        public const string JWTSecurityAlgorithm = Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256;
        public const string DefaultConnection = nameof(DefaultConnection);
        public const string JwtConfiguration = nameof(JwtConfiguration);
    }
}
