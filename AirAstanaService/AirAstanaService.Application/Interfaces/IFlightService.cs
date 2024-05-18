using AirAstanaService.Domain.Entities;
using AirAstanaService.Application.DTOs;

namespace AirAstanaService.Application.Interfaces;

public interface IFlightService
{
    Task<FlightDTO> AddFlightAsync(FlightDTO flightDto);
    Task UpdateFlightAsync(FlightDTO flightDto);
    Task DeleteFlightAsync(int id);
    Task<IEnumerable<FlightDTO>> GetFlightsAsync(string origin, string destination);
}