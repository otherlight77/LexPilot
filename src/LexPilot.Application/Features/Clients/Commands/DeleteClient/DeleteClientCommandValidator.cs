using FluentValidation;

namespace LexPilot.Application.Features.Clients.Commands.DeleteClient;

public sealed class DeleteClientCommandValidator : AbstractValidator<DeleteClientCommand>
{
    public DeleteClientCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
