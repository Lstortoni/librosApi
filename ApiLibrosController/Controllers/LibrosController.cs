using ApiLibrosController.Contracts.Dto;
using ApiLibrosController.Contracts.shared;
using ApiLibrosController.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiLibrosController.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibrosController : ControllerBase
    {
        private readonly ILibroService _service;

        public LibrosController(ILibroService service)
        {
            _service = service;
        }

        // POST api/libros
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<LibroDetailDto>> Create([FromBody] CreateLibroDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var created = await _service.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // GET api/libros/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<LibroDetailDto>> GetById([FromRoute] int id, CancellationToken ct)
        {
            var libro = await _service.GetByIdAsync(id, ct);
            if (libro is null) return NotFound();
            return Ok(libro);
        }

        // GET api/libros?term=...&pageNumber=1&pageSize=20&estado=1&sortBy=Precio_ASC
        [HttpGet]
        public async Task<ActionResult<PagedResult<LibroListItemDto>>> List([FromQuery] ListLibrosQueryDto query, CancellationToken ct)
        {
            var result = await _service.ListAsync(query, ct);
            return Ok(result);
        }

        // PUT api/libros/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateLibroDto dto, CancellationToken ct)
        {
            var ok = await _service.UpdateAsync(id, dto, ct);
            if (!ok) return NotFound();
            return NoContent();
        }

        // POST api/libros/{id}/publicar
        [Authorize]
        [HttpPost("{id:int}/publicar")]
        public async Task<ActionResult> Publish([FromRoute] int id, CancellationToken ct)
        {
            var ok = await _service.PublishAsync(id, ct);
            if (!ok) return NotFound();
            return NoContent();
        }

        // GET /api/libros/all
        [HttpGet("all")]
        public async Task<ActionResult<IReadOnlyList<LibroListItemDto>>> ListAll([FromQuery] ListLibrosQueryDto filter, CancellationToken ct)
        {
            var items = await _service.ListAllAsync(filter, ct);
            return Ok(items);
        }
    }
}
