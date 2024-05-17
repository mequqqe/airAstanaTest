using AirAstanaService.Application.DTOs;
using AirAstanaService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AirAstanaService.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        var token = await _authService.Authenticate(userLoginDto);

        if (token == null)
        {
            return Unauthorized();
        }

        return Ok(new { Token = token });
    }
}