using Data_Access_Layer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApiServer.Models;

namespace WebApiServer.Providers
{
    public class JwtTokenProvider
    {

        private readonly JwtConfiguration _jwtConfiguration;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public JwtTokenProvider(
            IOptions<JwtConfiguration> jwtConfiguration,
            UserManager<User> userManager,
            RoleManager<Role> roleManager
            )
        {
            _jwtConfiguration = jwtConfiguration.Value;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<string> CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName ?? ""),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(_jwtConfiguration.ByteKey);
            var credentials = new SigningCredentials(key, Constants.Constants.JWTSecurityAlgorithm);
            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync(userRole);
                var roleClaims = await _roleManager.GetClaimsAsync(role);

                foreach (var roleClaim in roleClaims)
                {
                    claims.Add(roleClaim);
                }
            }

            var userClaims = await _userManager.GetClaimsAsync(user);

            foreach (var userClaim in userClaims)
            {
                if(!claims
                    .Any(cl => cl.Type == userClaim.Type 
                        && cl.Value == userClaim.Value))
                {
                    claims.Add(userClaim);
                }
            }

            var token = new JwtSecurityToken(
                _jwtConfiguration.Issuer,
                _jwtConfiguration.Issuer,
                claims,
                expires: _jwtConfiguration.Expires,
                signingCredentials: credentials
            );


            var tokenResult = new JwtSecurityTokenHandler().WriteToken(token);
            
            return tokenResult;
        }
    }
}
