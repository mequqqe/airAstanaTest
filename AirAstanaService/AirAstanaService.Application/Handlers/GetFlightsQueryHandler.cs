using AirAstanaService.Application.DTOs;
using AirAstanaService.Application.Interfaces;
using AirAstanaService.Application.Queries;
using AirAstanaService.Domain.Entities;
using MediatR;

namespace AirAstanaService.Application.Handlers;

public class GetFlightsQueryHandler : IRequestHandler<GetFlightsQuery, IEnumerable<FlightDTO>>
{
    private readonly IFlightService _flightService;

    public GetFlightsQueryHandler(IFlightService flightService)
    {
        _flightService = flightService;
    }

    public async Task<IEnumerable<FlightDTO>> Handle(GetFlightsQuery request, CancellationToken cancellationToken)
    {
        return await _flightService.GetFlightsAsync(request.Origin, request.Destination);
    }
}
