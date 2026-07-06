using FluentValidation;

namespace LexPilot.Application.Features.Clients.Commands.UpdateClient;

public sealed class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
{
    public UpdateClientCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Nom)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Prenom)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Telephone)
            .MaximumLength(30);
    }
}
