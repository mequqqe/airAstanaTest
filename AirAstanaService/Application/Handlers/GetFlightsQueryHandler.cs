using AirAstanaService.Application.Interfaces;
using AirAstanaService.Application.Queries;
using AirAstanaService.Domain.Entities;
using MediatR;

namespace AirAstanaService.Application.Handlers;

public class GetFlightsQueryHandler : IRequestHandler<GetFlightsQuery, IEnumerable<Flight>>
{
    private readonly IFlightRepository _flightRepository;

    public GetFlightsQueryHandler(IFlightRepository flightRepository)
    {
        _flightRepository = flightRepository;
    }

    public async Task<IEnumerable<Flight>> Handle(GetFlightsQuery request, CancellationToken cancellationToken)
    {
        return await _flightRepository.GetFlightsAsync(request.Origin, request.Destination);
    }
}