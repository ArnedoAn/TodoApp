using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Services;
using TodoApp.Domain.Entities;

namespace TodoApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/todo")]
    public class TodoController(TodoService todoService) : ControllerBase
    {
        private readonly TodoService _todoService = todoService;

        /// <summary>
        /// Obtiene todas las tareas.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _todoService.GetAllAsync();
            return Ok(items);
        }

        /// <summary> 
        /// Obtiene una tarea por su ID. 
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _todoService.GetByIdAsync(id);
            if (item == null)
                return NotFound(new { Message = $"No se encontró la tarea con ID = {id}" });

            return Ok(item);
        }

        /// <summary>
        ///  Crea una nueva tarea. 
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TodoItem item)
        {
            try
            {
                var newItem = await _todoService.CreateAsync(item);
                return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Actualiza una tarea. 
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TodoItem item)
        {
            try
            {
                await _todoService.UpdateAsync(item);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
        }

        /// <summary> 
        /// Marca una tarea como completada. 
        /// </summary>
        [HttpPatch("{id:int}/complete")]
        public async Task<IActionResult> MarkAsCompleted(int id)
        {
            try
            {
                await _todoService.MarkAsCompletedAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
        }

        /// <summary> 
        /// Elimina una tarea.
        /// </summary>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _todoService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { ex.Message });
            }
        }
    }
}
