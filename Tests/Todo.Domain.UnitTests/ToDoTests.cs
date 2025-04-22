using Todo.Domain.Entities;
using Todo.Domain.Enums;

namespace Todo.Domain.UnitTests;

public class ToDoTests
{
    [Fact]
    public void Create_ShouldBeSuccessful()
    {
        var id = Guid.NewGuid();
        var title = "Foo";
        var description = "Bar";
        var now = DateTime.UtcNow;

        var todo = new ToDo(id, title, description, now);

        todo.Id.ShouldBe(id);
        todo.Title.ShouldBe(title);
        todo.Description.ShouldBe(description);
        todo.CreatedAt.ShouldBe(now);
        todo.Status.ShouldBe(ToDoStatus.Created);
        todo.UpdatedAt.ShouldBeNull();
    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000", "Foo")]
    [InlineData("c0faa371-256e-4189-8cc7-4eeb00e77327", "")]
    [InlineData("c0faa371-256e-4189-8cc7-4eeb00e77327", null)]
    public void Create_WithInvalidValues_ShouldBeFailed(string stringId, string? title)
    {
        var id = Guid.Parse(stringId);

        void Action()
        {
            _ = new ToDo(id, title!, null, DateTime.UtcNow);
        }

        Should.Throw<ArgumentException>(Action);
    }

    [Fact]
    public void Todo_ShouldBeSuccessful()
    {
        var todo = new ToDo(Guid.NewGuid(), "Foo", "Bar", DateTime.UtcNow);

        var now = DateTime.UtcNow;
        todo.Todo(now);

        todo.Status.ShouldBe(ToDoStatus.ToDo);
        todo.UpdatedAt.ShouldBe(now);
    }

    [Fact]
    public void Done_ShouldBeSuccessful()
    {
        var todo = new ToDo(Guid.NewGuid(), "Foo", "Bar", DateTime.UtcNow);
        var now = DateTime.UtcNow;
        todo.Todo(now);

        now = DateTime.UtcNow;
        todo.Done(now);

        todo.Status.ShouldBe(ToDoStatus.Done);
        todo.UpdatedAt.ShouldBe(now);
    }

    [Fact]
    public void Done_WithInvalidStatus_ShouldBeFailed()
    {
        var todo = new ToDo(Guid.NewGuid(), "Foo", "Bar", DateTime.UtcNow);

        var now = DateTime.UtcNow;
        void Action() => todo.Done(now);

        Should.Throw<ArgumentException>(Action);
    }

    [Fact]
    public void Update_ShouldBeSuccessful()
    {
        var todo = new ToDo(Guid.NewGuid(), "Foo", "Bar", DateTime.UtcNow);

        var now = DateTime.UtcNow;
        todo.Update("Bar", "Foo", now);

        todo.Title.ShouldBe("Bar");
        todo.Description.ShouldBe("Foo");
        todo.UpdatedAt.ShouldBe(now);
    }

    [Fact]
    public void Update_WithInvalidValues_ShouldBeFailed()
    {
        var todo = new ToDo(Guid.NewGuid(), "Foo", "Bar", DateTime.UtcNow);

        void Action()
        {
            var now = DateTime.UtcNow;
            todo.Update(null!, "Foo", now);
        }

        Should.Throw<ArgumentException>(Action);
    }
}
