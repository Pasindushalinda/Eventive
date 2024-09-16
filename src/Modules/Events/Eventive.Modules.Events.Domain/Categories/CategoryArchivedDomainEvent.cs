﻿using Eventive.Modules.Events.Domain.Abstractions;

namespace Eventive.Modules.Events.Domain.Categories;

public sealed class CategoryArchivedDomainEvent(Guid categoryId) : DomainEvent
{
    public Guid CategoryId { get; init; } = categoryId;
}