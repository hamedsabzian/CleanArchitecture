namespace Todo.Application.Common;

internal interface IIdGenerator
{
    public Guid New<TEntity>();
}

internal class IdGenerator : IIdGenerator
{
    public Guid New<TEntity>()
    {
        return Guid.NewGuid();
    }
}
