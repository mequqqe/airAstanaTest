using AirAstanaService.Application.DTOs;
using AirAstanaService.Domain.Entities;
using MediatR;

namespace AirAstanaService.Application.Commands
{
    public class AddFlightCommand : IRequest<FlightDTO>
    {
        public FlightDTO Flight { get; set; }
    }
}
