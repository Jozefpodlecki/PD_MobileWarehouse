using Common.Providers;
using Data_Access_Layer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebApiServer.Providers
{
    public class JwtTokenProvider : IJwtTokenProvider
    {
        private readonly JwtConfiguration _jwtConfiguration;

        public JwtTokenProvider(
            IOptions<JwtConfiguration> jwtConfiguration
            )
        {
            _jwtConfiguration = jwtConfiguration.Value;
        }

        public string CreateToken(
            User user,
            IList<Claim> permissionClaims
            )
        {
            var currentDate = DateTimeOffset.Now;
            var issuedAt = currentDate.ToUnixTimeSeconds().ToString();
            var expirationTime = currentDate.AddSeconds(_jwtConfiguration.Expires).ToUnixTimeSeconds().ToString();
            var userId = user.Id.ToString();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName ?? ""),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName ?? ""),
                new Claim(JwtRegisteredClaimNames.Iat, issuedAt),
                new Claim(JwtRegisteredClaimNames.Exp, expirationTime),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var key = new SymmetricSecurityKey(_jwtConfiguration.ByteKey);
            var credentials = new SigningCredentials(key, Constants.JWTSecurityAlgorithm);

            claims.AddRange(permissionClaims);

            var token = new JwtSecurityToken(
                _jwtConfiguration.Issuer,
                _jwtConfiguration.Issuer,
                claims,
                signingCredentials: credentials
            );

            var tokenResult = new JwtSecurityTokenHandler().WriteToken(token);
            
            return tokenResult;
        }
    }
}