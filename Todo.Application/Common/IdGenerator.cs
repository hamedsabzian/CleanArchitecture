namespace Todo.Application.Common;

public interface IIdGenerator
{
    public Guid New<TEntity>();
}

public class IdGenerator : IIdGenerator
{
    public Guid New<TEntity>()
    {
        return Guid.NewGuid();
    }
}
