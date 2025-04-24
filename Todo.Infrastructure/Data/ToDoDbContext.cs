using Microsoft.EntityFrameworkCore;
using Todo.Application.Shared.Interfaces;
using Todo.Domain.Entities;
using Todo.Domain.Entities.Common;

namespace Todo.Infrastructure.Data;

public class ToDoDbContext : DbContext
{
    private readonly IDomainEventDispatcher? _domainEventDispatcher;

    public ToDoDbContext(DbContextOptions<ToDoDbContext> options, IDomainEventDispatcher domainEventDispatcher) : base(options)
    {
        _domainEventDispatcher = domainEventDispatcher;
    }

    protected ToDoDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ToDo> Todos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        // ignore events if no dispatcher provided
        if (_domainEventDispatcher == null)
        {
            return result;
        }

        // dispatch events only if save was successful
        var entitiesWithEvents = ChangeTracker.Entries<HasDomainEventsBase>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToArray();

        await _domainEventDispatcher.DispatchAndClearEvents(entitiesWithEvents);

        return result;
    }

    public override int SaveChanges() => SaveChangesAsync().GetAwaiter().GetResult();
}
