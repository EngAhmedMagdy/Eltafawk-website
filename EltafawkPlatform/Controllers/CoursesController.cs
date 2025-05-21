using EltafawkPlatform.Dto;
using EltafawkPlatform.Mapper;
using EltafawkPlatform.Models;
using EltafawkPlatform.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EltafawkPlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly CourseService _courseService;
        private readonly IWebHostEnvironment _env;

        public CoursesController(CourseService courseService, IWebHostEnvironment env)
        {
            _courseService = courseService;
            _env = env;
        }

        [HttpGet]
        public async Task<ActionResult<List<CourseUploadDto>>> GetAll()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            var dtos = courses.Select(c => c.ToDto()).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseUploadDto>> GetById(string id)
        {
            var model = await _courseService.GetCourseByIdAsync(id);
            if (model == null)
                return NotFound();

            return Ok(model.ToDto());
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CourseUploadDto dto)
        {
            var model = dto.ToModel();
            await _courseService.CreateCourseAsync(model);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] CourseUploadDto dto)
        {
            var existing = await _courseService.GetCourseByIdAsync(id);
            if (existing == null)
                return NotFound();

            existing.UpdateModel(dto);
            await _courseService.UpdateCourseAsync(id,existing);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var deleted = await _courseService.DeleteCourseAsync(id);
            if (!deleted)
                return NotFound();

            return Ok();
        }
        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var uploadsPath = Path.Combine(_env.WebRootPath, "images");
            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsPath, fileName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            var relativePath = $"/images/{fileName}";

            return Ok(new UploadResultDto{ Path = relativePath });
        }

    }


}
