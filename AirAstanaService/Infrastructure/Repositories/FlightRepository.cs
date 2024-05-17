using AirAstanaService.Application.Interfaces;
using AirAstanaService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirAstanaService.Infrastructure.Repositories;

public class FlightRepository : IFlightRepository
{
    private readonly FlightDbContext _context;

    public FlightRepository(FlightDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Flight>> GetFlightsAsync(string origin, string destination)
    {
        var query = _context.Flights.AsQueryable();

        if (!string.IsNullOrEmpty(origin))
        {
            query = query.Where(f => f.Origin == origin);
        }

        if (!string.IsNullOrEmpty(destination))
        {
            query = query.Where(f => f.Destination == destination);
        }

        return await query.ToListAsync();
    }

    public async Task<Flight> GetFlightByIdAsync(int id)
    {
        return await _context.Flights.FindAsync(id);
    }

    public async Task AddFlightAsync(Flight flight)
    {
        _context.Flights.Add(flight);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateFlightAsync(Flight flight)
    {
        _context.Entry(flight).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteFlightAsync(int id)
    {
        var flight = await _context.Flights.FindAsync(id);
        if (flight != null)
        {
            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
        }
    }
}