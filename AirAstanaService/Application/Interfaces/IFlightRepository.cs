using AirAstanaService.Domain.Entities;

namespace AirAstanaService.Application.Interfaces;

public interface IFlightRepository
{
    Task<IEnumerable<Flight>> GetFlightsAsync(string origin, string destination);
    Task<Flight> GetFlightByIdAsync(int id);
    Task AddFlightAsync(Flight flight);
    Task UpdateFlightAsync(Flight flight);
    Task DeleteFlightAsync(int id);
}