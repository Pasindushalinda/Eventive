using FluentValidation;
using System.Data;

namespace Eventive.Modules.Events.Application.Events.CreateEvent;

internal sealed class CreateEventCommandValidator:AbstractValidator<CreateEventCommand>
{
    public CreateEventCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Location).NotEmpty();
        RuleFor(x => x.StartAtUtc).NotEmpty();
        RuleFor(x => x.EndAtUtc).Must((cmd,endAtUtc)=>endAtUtc>cmd.StartAtUtc)
            .When(x=>x.EndAtUtc.HasValue);
    }
}
