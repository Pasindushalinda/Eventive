using FluentValidation;

namespace Eventive.Modules.Events.Application.Events.CreateEvent;

internal sealed class CreateEventCommandValidator:AbstractValidator<CreateEventCommand>
{
    //inject repository and validate the before hit the usecase also possible here
    public CreateEventCommandValidator()
    {
        RuleFor(c => c.CategoryId).NotEmpty();
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Location).NotEmpty();
        RuleFor(x => x.StartsAtUtc).NotEmpty();
        RuleFor(x => x.EndsAtUtc).Must((cmd,EndsAtUtc)=>EndsAtUtc>cmd.StartsAtUtc)
            .When(x=>x.EndsAtUtc.HasValue);
    }
}
