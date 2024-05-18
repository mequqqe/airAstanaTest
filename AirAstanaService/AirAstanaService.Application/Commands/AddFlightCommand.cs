using AirAstanaService.Domain.Entities;
using MediatR;

namespace AirAstanaService.Application.Commands;

public class AddFlightCommand : IRequest<Flight>
{
    public string Origin { get; set; }
    public string Destination { get; set; }
    public DateTimeOffset Departure { get; set; }
    public DateTimeOffset Arrival { get; set; }
    public FlightStatus Status { get; set; }
}