using AirAstanaService.Application.DTOs;

namespace AirAstanaService.Application.Interfaces;

public interface IAuthService
{
    Task<string> Authenticate(UserLoginDto userLoginDto);
}