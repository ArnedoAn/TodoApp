using System.Threading.Tasks;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Services;
using TodoApp.Domain.Entities;
using Moq;
using Xunit;

namespace TodoApp.Test.Services
{
    public class TodoServiceTests
    {
        private readonly Mock<ITodoRepository> _repositoryMock;
        private readonly TodoService _todoService;

        public TodoServiceTests()
        {
            _repositoryMock = new Mock<ITodoRepository>();
            _todoService = new TodoService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetById_ShouldReturnItem_WhenItemExists()
        {
            var todo = new TodoItem { Id = 1, Title = "Test Task", IsComplete = false };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(todo);

            var result = await _todoService.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task Create_ShouldReturnNewItem_WhenValid()
        {
            var todo = new TodoItem { Title = "New Task", IsComplete = false };
            var savedTodo = new TodoItem { Id = 2, Title = "New Task", IsComplete = false };

            _repositoryMock.Setup(repo => repo.AddAsync(todo)).ReturnsAsync(savedTodo);

            var result = await _todoService.CreateAsync(todo);

            Assert.NotNull(result);
            Assert.Equal(2, result.Id);
        }

        [Fact]
        public async Task MarkAsCompleted_ShouldUpdateTask_WhenTaskExists()
        {
            var todo = new TodoItem { Id = 3, Title = "Incomplete Task", IsComplete = false };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(3)).ReturnsAsync(todo);

            await _todoService.MarkAsCompletedAsync(3);

            Assert.True(todo.IsComplete);
            _repositoryMock.Verify(repo => repo.UpdateAsync(todo), Times.Once);
        }
    }

}

