using AirAstanaService.Domain.Entities;
using MediatR;

namespace AirAstanaService.Application.Commands;

public class UpdateFlightCommand : IRequest<Unit>
{
    public Flight Flight { get; set; }
}