using AirAstanaService.Application.Commands;
using AirAstanaService.Application.DTOs;
using AirAstanaService.Application.Interfaces;
using AirAstanaService.Domain.Entities;
using MediatR;

namespace AirAstanaService.Application.Handlers;

public class AddFlightCommandHandler : IRequestHandler<AddFlightCommand, FlightDTO>
{
    private readonly IFlightService _flightService;

    public AddFlightCommandHandler(IFlightService flightService)
    {
        _flightService = flightService;
    }

    public async Task<FlightDTO> Handle(AddFlightCommand request, CancellationToken cancellationToken)
    {
        return await _flightService.AddFlightAsync(request.Flight);
    }
}
