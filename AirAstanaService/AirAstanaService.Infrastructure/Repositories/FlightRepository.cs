using AirAstanaService.Application.Interfaces;
using AirAstanaService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AirAstanaService.Infrastructure.Repositories;

public class FlightRepository : IFlightRepository
{
    private readonly FlightDbContext _context;
    private readonly ILogger<FlightRepository> _logger;

    public FlightRepository(FlightDbContext context, ILogger<FlightRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Flight>> GetFlightsAsync(string origin, string destination)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Произошла ошибка при получении рейсов с отправлением из: {Origin} и прибытием в: {Destination}", origin, destination);
            throw;
        }
    }

    public async Task<Flight> GetFlightByIdAsync(int id)
    {
        try
        {
            return await _context.Flights.FindAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Произошла ошибка при получении рейса с ID: {Id}", id);
            throw;
        }
    }

    public async Task AddFlightAsync(Flight flight)
    {
        try
        {
            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Произошла ошибка при добавлении рейса: {@Flight}", flight);
            throw;
        }
    }

    public async Task UpdateFlightAsync(Flight flight)
    {
        try
        {
            _context.Entry(flight).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Произошла ошибка при обновлении рейса: {@Flight}", flight);
            throw;
        }
    }

    public async Task DeleteFlightAsync(int id)
    {
        try
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Произошла ошибка при удалении рейса с ID: {Id}", id);
            throw;
        }
    }
}
