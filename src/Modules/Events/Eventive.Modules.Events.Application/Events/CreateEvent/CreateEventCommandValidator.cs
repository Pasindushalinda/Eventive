using FluentValidation;

namespace Eventive.Modules.Events.Application.Events.CreateEvent;

internal sealed class CreateEventCommandValidator:AbstractValidator<CreateEventCommand>
{
    //inject repository and validate the before hit the usecase also possible here
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
