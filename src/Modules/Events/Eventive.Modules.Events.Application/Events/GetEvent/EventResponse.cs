namespace Eventive.Modules.Events.Application.Events.GetEvent;

public sealed record EventResponse
(
    Guid Id,
    string Title,
    string Description,
    string Location,
    DateTime StartAtUtc,
    DateTime EndAtUtc
);