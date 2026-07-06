using MediatR;

namespace LexPilot.Application.Features.Clients.Commands.DeleteClient;

public sealed record DeleteClientCommand(Guid Id) : IRequest;
