using EltafawkPlatform.Dto;
using EltafawkPlatform.Mapper;
using EltafawkPlatform.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EltafawkPlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;

        public UsersController(UserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ApplicationUserDto>>> GetAll() {
            var users = await _service.GetAllAsync();
            var dtos = users.Select(c => c.ToDto()).ToList();
            return Ok(dtos);
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUserDto>> GetById(string id)
        {
            var model = await _service.GetByIdAsync(id);
            return model == null ? NotFound() : Ok(model.ToDto());
        }

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] ApplicationUserDto dto)
        //{
        //    await _service.CreateAsync(dto);
        //    return Ok();
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ApplicationUserDto dto)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            existing.UpdateModel(dto);
            await _service.UpdateAsync(id, existing);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }

}
