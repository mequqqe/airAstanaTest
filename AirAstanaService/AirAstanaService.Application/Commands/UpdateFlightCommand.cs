using AirAstanaService.Application.DTOs;
using AirAstanaService.Domain.Entities;
using MediatR;

namespace AirAstanaService.Application.Commands;

public class UpdateFlightCommand : IRequest<Unit>
{
    public FlightDTO Flight { get; set; }
}
