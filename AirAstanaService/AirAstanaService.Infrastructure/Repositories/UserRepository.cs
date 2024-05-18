using AirAstanaService.Application.Interfaces;
using AirAstanaService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirAstanaService.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly FlightDbContext _context;

    public UserRepository(FlightDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.Include(u => u.Role)
            .SingleOrDefaultAsync(u => u.Username == username);
    }
}