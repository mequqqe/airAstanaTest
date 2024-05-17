using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AirAstanaService.Application.DTOs;
using AirAstanaService.Application.Interfaces;
using AirAstanaService.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace AirAstanaService.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<string> Authenticate(UserLoginDto userLoginDto)
    {
        var user = await _userRepository.GetUserByUsernameAsync(userLoginDto.Username);

        if (user == null || user.Password != userLoginDto.Password) // Добавьте хэширование паролей
        {
            return null;
        }

        var token = GenerateJwtToken(user);
        return token;
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, user.Role.Code)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
