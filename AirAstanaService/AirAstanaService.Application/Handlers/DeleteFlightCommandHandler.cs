using AirAstanaService.Application.Commands;
using AirAstanaService.Application.Interfaces;
using MediatR;

namespace AirAstanaService.Application.Handlers;

public class DeleteFlightCommandHandler : IRequestHandler<DeleteFlightCommand, Unit>
{
    private readonly IFlightService _flightService;

    public DeleteFlightCommandHandler(IFlightService flightService)
    {
        _flightService = flightService;
    }

    public async Task<Unit> Handle(DeleteFlightCommand request, CancellationToken cancellationToken)
    {
        await _flightService.DeleteFlightAsync(request.Id);
        return Unit.Value;
    }
}
