using Microsoft.EntityFrameworkCore;
using Todo.Infrastructure.Data;

namespace Todo.Application.IntegratedTests.Common;

public class ToDoTestDbContext : ToDoDbContext
{
    public ToDoTestDbContext(DbContextOptions<ToDoTestDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
