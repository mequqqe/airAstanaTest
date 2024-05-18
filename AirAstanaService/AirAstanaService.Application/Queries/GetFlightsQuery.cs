using AirAstanaService.Application.DTOs;
using AirAstanaService.Domain.Entities;
using MediatR;

namespace AirAstanaService.Application.Queries;

public class GetFlightsQuery : IRequest<IEnumerable<FlightDTO>>
{
    public string Origin { get; set; }
    public string Destination { get; set; }
}


