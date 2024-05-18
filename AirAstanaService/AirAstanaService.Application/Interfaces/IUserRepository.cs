using AirAstanaService.Domain.Entities;

namespace AirAstanaService.Application.Interfaces;

public interface IUserRepository
{
    Task<User> GetUserByUsernameAsync(string username);
}