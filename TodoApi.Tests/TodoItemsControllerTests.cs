using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TodoApi.Controllers;
using TodoApi.Data;
using TodoApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace TodoApi.Tests
{
    public class TodoItemsControllerTests
    {
        private TodoContext GetInMemoryDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new TodoContext(options);
        }

        [Fact]
        public async Task GetTodoItems_ShouldReturnAllItems()
        {
            var context = GetInMemoryDbContext(nameof(GetTodoItems_ShouldReturnAllItems));
            context.TodoItems.Add(new TodoItem { Title = "Test 1", Description = "Desc 1" });
            context.TodoItems.Add(new TodoItem { Title = "Test 2", Description = "Desc 2" });
            await context.SaveChangesAsync();

            var controller = new TodoItemsController(context);
            var result = await controller.GetTodoItems();

            result.Value.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetTodoItem_ShouldReturnItem_IfExists()
        {
            var context = GetInMemoryDbContext(nameof(GetTodoItem_ShouldReturnItem_IfExists));
            var item = new TodoItem { Title = "Test", Description = "Desc" };
            context.TodoItems.Add(item);
            await context.SaveChangesAsync();

            var controller = new TodoItemsController(context);
            var result = await controller.GetTodoItem(item.Id);

            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(item.Id);
            result.Value.Title.Should().Be("Test");
        }

        [Fact]
        public async Task GetTodoItem_ShouldReturnNotFound_IfNotExists()
        {
            var context = GetInMemoryDbContext(nameof(GetTodoItem_ShouldReturnNotFound_IfNotExists));
            var controller = new TodoItemsController(context);

            var result = await controller.GetTodoItem(999);

            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task PostTodoItem_ShouldCreateItem()
        {
            var context = GetInMemoryDbContext(nameof(PostTodoItem_ShouldCreateItem));
            var controller = new TodoItemsController(context);
            var item = new TodoItem { Title = "New Task", Description = "Details" };

            var result = await controller.PostTodoItem(item);

            result.Result.Should().BeOfType<CreatedAtActionResult>();
            context.TodoItems.Count().Should().Be(1);
        }

        [Fact]
        public async Task PutTodoItem_ShouldUpdateItem_IfExists()
        {
            var context = GetInMemoryDbContext(nameof(PutTodoItem_ShouldUpdateItem_IfExists));
            var item = new TodoItem { Title = "Old", Description = "OldDesc" };
            context.TodoItems.Add(item);
            await context.SaveChangesAsync();

            var controller = new TodoItemsController(context);
            item.Title = "Updated";

            var result = await controller.PutTodoItem(item.Id, item);

            result.Should().BeOfType<NoContentResult>();
            context.TodoItems.Find(item.Id)?.Title.Should().Be("Updated");
        }

        [Fact]
        public async Task PutTodoItem_ShouldReturnBadRequest_IfIdMismatch()
        {
            var context = GetInMemoryDbContext(nameof(PutTodoItem_ShouldReturnBadRequest_IfIdMismatch));
            var controller = new TodoItemsController(context);
            var item = new TodoItem { Id = 1, Title = "Mismatch", Description = "Error" };

            var result = await controller.PutTodoItem(2, item);

            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task DeleteTodoItem_ShouldRemoveItem_IfExists()
        {
            var context = GetInMemoryDbContext(nameof(DeleteTodoItem_ShouldRemoveItem_IfExists));
            var item = new TodoItem { Title = "Delete Me", Description = "To Be Removed" };
            context.TodoItems.Add(item);
            await context.SaveChangesAsync();

            var controller = new TodoItemsController(context);
            var result = await controller.DeleteTodoItem(item.Id);

            result.Should().BeOfType<NoContentResult>();
            context.TodoItems.Find(item.Id).Should().BeNull();
        }

        [Fact]
        public async Task DeleteTodoItem_ShouldReturnNotFound_IfNotExists()
        {
            var context = GetInMemoryDbContext(nameof(DeleteTodoItem_ShouldReturnNotFound_IfNotExists));
            var controller = new TodoItemsController(context);

            var result = await controller.DeleteTodoItem(123);

            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
