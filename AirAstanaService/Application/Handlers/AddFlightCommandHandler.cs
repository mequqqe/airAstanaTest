using AirAstanaService.Application.Commands;
using AirAstanaService.Application.Interfaces;
using AirAstanaService.Domain.Entities;
using MediatR;

namespace AirAstanaService.Application.Handlers;

public class AddFlightCommandHandler : IRequestHandler<AddFlightCommand, Flight>
{
    private readonly IFlightRepository _flightRepository;

    public AddFlightCommandHandler(IFlightRepository flightRepository)
    {
        _flightRepository = flightRepository;
    }

    public async Task<Flight> Handle(AddFlightCommand request, CancellationToken cancellationToken)
    {
        var flight = new Flight
        {
            Origin = request.Origin,
            Destination = request.Destination,
            Departure = request.Departure,
            Arrival = request.Arrival,
            Status = request.Status
        };

        await _flightRepository.AddFlightAsync(flight);
        return flight;
    }
}