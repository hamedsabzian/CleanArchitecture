namespace Todo.Domain.Entities.Common;

public abstract class EntityBase<TId> : HasDomainEventsBase
    where TId : struct, IEquatable<TId>
{
    public TId Id { get; set; } = default!;
}