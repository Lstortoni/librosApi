using ApiLibrosController.Contracts.Dto;
using ApiLibrosController.Contracts.shared;
using ApiLibrosController.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiLibrosController.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendedorController : ControllerBase
    {
        private readonly IVendedorService _service;

        public VendedorController(IVendedorService service)
        {
            _service = service;
        }

        // POST api/vendedores
        [HttpPost]
        public async Task<ActionResult<VendedorBasicDto>> Create([FromBody] CreateVendedorDto dto, CancellationToken ct)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var created = await _service.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetBasicById), new { id = created.Id }, created);
        }

        // GET api/vendedores/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<VendedorBasicDto>> GetBasicById([FromRoute] int id, CancellationToken ct)
        {
            var vendedor = await _service.GetBasicByIdAsync(id, ct);
            if (vendedor is null) return NotFound();
            return Ok(vendedor);
        }

        // GET api/vendedores (opcional si implementaste List en el service)
        [HttpGet]
        public async Task<ActionResult<PagedResult<VendedorBasicDto>>> List([FromQuery] ListVendedorQueryDto query, CancellationToken ct)
        {
            var result = await _service.ListAsync(query, ct);
            return Ok(result);
        }

        // GET /api/vendedores/all
        [HttpGet("all")]
        public async Task<ActionResult<IReadOnlyList<VendedorBasicDto>>> ListAll([FromQuery] string? term, CancellationToken ct)
        {
            var items = await _service.ListAllAsync(term, ct);
            return Ok(items);
        }
    }
}
