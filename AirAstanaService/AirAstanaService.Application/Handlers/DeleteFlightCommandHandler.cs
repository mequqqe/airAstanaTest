using AirAstanaService.Application.Commands;
using AirAstanaService.Application.Interfaces;
using MediatR;

namespace AirAstanaService.Application.Handlers;

public class DeleteFlightCommandHandler : IRequestHandler<DeleteFlightCommand, Unit>
{
    private readonly IFlightRepository _flightRepository;

    public DeleteFlightCommandHandler(IFlightRepository flightRepository)
    {
        _flightRepository = flightRepository;
    }

    public async Task<Unit> Handle(DeleteFlightCommand request, CancellationToken cancellationToken)
    {
        await _flightRepository.DeleteFlightAsync(request.Id);
        return Unit.Value;
    }
}