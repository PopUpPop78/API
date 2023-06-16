using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Data.Contracts;
using Data.Security;
using Data.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Data.Repositories
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<CoreUser> _userManager;
        private readonly ILogger<AuthManager> _logger;
        private readonly JwtSettings _jwtSettings;
        private CoreUser user;

        public AuthManager(IMapper mapper, UserManager<CoreUser> userManager, ILogger<AuthManager> logger, IOptions<JwtSettings> jwtSecrets)
        {
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;
            _jwtSettings = jwtSecrets.Value;
        }

        public async Task<string> CreateRefreshToken()
        {
            await _userManager.RemoveAuthenticationTokenAsync(user, _jwtSettings.ExpenseApiIssuer, SD.RefreshToken);
            var newRefreshToken = await _userManager.GenerateUserTokenAsync(user, _jwtSettings.ExpenseApiIssuer, SD.RefreshToken);
            var result = await _userManager.SetAuthenticationTokenAsync(user, _jwtSettings.ExpenseApiIssuer, SD.RefreshToken, newRefreshToken);

            return newRefreshToken;
        }

        public async Task<AuthUser> Login(LoginUser loginUser)
        {
            user = await _userManager.FindByEmailAsync(loginUser.Email);
            var isValid = await _userManager.CheckPasswordAsync(user, loginUser.Password);
            
            if(user == null || !isValid)
            {
                return null;
            }

            return new AuthUser
            {
                Token = await GenerateToken(),
                RefreshToken = await CreateRefreshToken()
            };
        }

        public async Task<IEnumerable<IdentityError>> Register(CreateUser createUser)
        {
            user = _mapper.Map<CoreUser>(createUser);
            user.UserName = createUser.Email;

            var result = await _userManager.CreateAsync(user, createUser.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, SD.RolesUser);
            }

            _logger.LogInformation(message: $"User {user.UserName} registered");
            return result.Errors;
        }

        public async Task<AuthUser> VerifyRefreshToken(AuthUser authUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var content = tokenHandler.ReadJwtToken(authUser.Token);
            var username = (from x in content.Claims where x.Type == JwtRegisteredClaimNames.Email select x).FirstOrDefault()?.Value;

            user = await _userManager.FindByEmailAsync(username);

            if (user == null)
                return null;

            if (await _userManager.VerifyUserTokenAsync(user, _jwtSettings.ExpenseApiIssuer, SD.RefreshToken, authUser.RefreshToken))
            {
                return new AuthUser
                {
                    Token = await GenerateToken(),
                    RefreshToken = await CreateRefreshToken()
                };
            }

            await _userManager.UpdateSecurityStampAsync(user);
            return null;
        }

        private async Task<string> GenerateToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.JwtSymmetricKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var roleClaims = (from x in await _userManager.GetRolesAsync(user) select new Claim(SD.Role, x)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(SD.LastFirstName, $"{user.LastName}, {user.FirstName}"),
                new Claim(SD.UserId, user.Id)
            }
            .Union(roleClaims)
            .Union(userClaims);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.ExpenseApiIssuer,
                audience: _jwtSettings.ExpenseApiAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: credentials
            );            

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
