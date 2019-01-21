using Common;
using Common.Providers;
using Data_Access_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Client.Providers.Mock
{
    public class JwtTokenProvider : IJwtTokenProvider
    {
        private readonly byte[] _byteKey;
        private readonly int _expires;

        public JwtTokenProvider(AppSettings appSettings)
        {
            _byteKey = appSettings.JwtConfiguration.ByteKey;
            _expires = appSettings.JwtConfiguration.Expires;
        }

        public string CreateToken(
            User user,
            IList<Claim> permissionClaims
            )
        {

            var currentDate = DateTimeOffset.Now;
            var issuedAt = currentDate.ToUnixTimeSeconds().ToString();
            var expirationTime = currentDate.AddSeconds(_expires).ToUnixTimeSeconds().ToString();
            var userId = user.Id.ToString();

            var payload = new Dictionary<string, object>
            {
                { ClaimTypes.NameIdentifier, userId },
                { "jti", Guid.NewGuid() },
                { "sub", userId },
                { "iat", issuedAt },
                { "Role", user.Role.Name },
                { "Permission", permissionClaims.Select(cl => cl.Value).ToList() },
                { "exp", expirationTime }
            };

            var token = JWT.JsonWebToken.Encode(payload, _byteKey, JWT.JwtHashAlgorithm.HS256);

            return token;
        }
    }
}