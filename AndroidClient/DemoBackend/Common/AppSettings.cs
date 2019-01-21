namespace Common
{
    public class AppSettings
    {
        public string AllowedHosts { get; set; }
        public string Url { get; set; }
        public JwtConfiguration JwtConfiguration { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
        public Logging Logging { get; set; }
    }

    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }

    public class Logging
    {
        public LogLevel LogLevel { get; set; }
    }

    public class LogLevel
    {
        public string Default { get; set; }
    }
}
