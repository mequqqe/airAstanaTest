using MediatR;

namespace AirAstanaService.Application.Commands;

public class DeleteFlightCommand : IRequest<Unit>
{
    public int Id { get; set; }
}