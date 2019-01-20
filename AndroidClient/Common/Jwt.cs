using Newtonsoft.Json;
using System.Security.Claims;

namespace WebApiServer.Models
{
    public class Jwt
    {
        [JsonProperty("sub")]
        public string sub { get; set; }

        [JsonProperty("given_name")]
        public string Name { get; set; }

        [JsonProperty("family_name")]
        public string Surname { get; set; }

        [JsonProperty("jti")]
        public string JwtId { get; set; }

        [JsonProperty("iat")]
        public string IssuedAt { get; set; }

        [JsonProperty("exp")]
        public string ExpirationTime { get; set; }

        [JsonProperty("iss")]
        public string Issuer { get; set; }

        [JsonProperty(ClaimTypes.NameIdentifier)]
        public string Id { get; set; }

        [JsonProperty("aud")]
        public string Audience { get; set; }

        [JsonProperty("Permission")]
        public string[] Claims { get; set; }
    }
}
