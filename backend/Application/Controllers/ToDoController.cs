using Microsoft.AspNetCore.Mvc;
using TodoApi.Application.CommandHandlers;
using TodoApi.Application.DTOs;
using TodoApi.Application.QueryHandlers;
using Microsoft.AspNetCore.Authorization;

namespace TodoApi.Application.Controllers
{
    [ApiController]
    [Route("api/todo")]
    [Authorize]
    public class ToDoController : ControllerBase
    {
        private readonly GetAllToDosHandler _getAllHandler;
        private readonly GetToDoByIdHandler _getByIdHandler;
        private readonly CreateToDoHandler _createHandler;
        private readonly UpdateToDoHandler _updateHandler;
        private readonly DeleteToDoHandler _deleteHandler;

        public ToDoController(
            GetAllToDosHandler getAllHandler,
            GetToDoByIdHandler getByIdHandler,
            CreateToDoHandler createHandler,
            UpdateToDoHandler updateHandler,
            DeleteToDoHandler deleteHandler)
        {
            _getAllHandler = getAllHandler;
            _getByIdHandler = getByIdHandler;
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sonuc = await _getAllHandler.Handle(new GetAllToDosQuery());
            return Ok(sonuc);

        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var sonuc = await _getByIdHandler.Handle(new GetToDoByIdQuery { Id = id });
                if (sonuc == null)
                    return NotFound("Id li görev bulunamadı");
            return Ok(sonuc);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateToDoDto dto)
        {
            
            var command = new CreateToDoCommand
            {
                Title = dto.Title,
                Description = dto.Description,
                IsCompleted = dto.IsCompleted
            };

            var sonuc = await _createHandler.Handle(command);
            return CreatedAtAction(nameof(GetById), new { id = sonuc.Id }, sonuc);
        }
       
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateToDoDto dto)
        {
            var command = new UpdateToDoCommand
            {
                Id = id,  
                Title = dto.Title,
                Description = dto.Description,
                IsCompleted = dto.IsCompleted
            };

            var basarili = await _updateHandler.Handle(command);

            if (!basarili)
                return NotFound($"{id} id'li görev bulunamadı");

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var basarili = await _deleteHandler.Handle(new DeleteToDoCommand { Id = id });

            if (!basarili)
                return NotFound($"{id} id'li görev bulunamadı");

            return NoContent();
        }
    }
}
