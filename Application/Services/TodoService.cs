using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Services
{
    public class TodoService(ITodoRepository repository)
    {
        private readonly ITodoRepository _repository = repository;

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TodoItem?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<TodoItem> CreateAsync(TodoItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Title))
                throw new ArgumentException("El título no puede estar vacío.");

            return await _repository.AddAsync(item);
        }

        public async Task UpdateAsync(TodoItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Title))
                throw new ArgumentException("El título no puede estar vacío.");

            await _repository.UpdateAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"No se encontró la tarea con ID = {id}");

            await _repository.DeleteAsync(existing);
        }

        /// <summary>
        /// Marca una tarea como completada.
        /// </summary>
        public async Task MarkAsCompletedAsync(int id)
        {
            var todo = await _repository.GetByIdAsync(id);
            if (todo == null)
                throw new KeyNotFoundException($"No se encontró la tarea con ID = {id}");

            todo.IsComplete = true;
            await _repository.UpdateAsync(todo);
        }
    }
}