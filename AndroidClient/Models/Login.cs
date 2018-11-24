namespace Client.Models
{
    public class Login
    {
        public string ServerName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}