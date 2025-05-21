using EltafawkPlatform.Models;
using EltafawkPlatform.Services;
using Microsoft.AspNetCore.Mvc;

namespace EltafawkPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(StudentModel student)
        {
            await _studentService.CreateAsync(student);
            return Ok(new ApiResponse { Success = true, Message = "Registered successfully" });
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new ApiResponse { Success = true, Message = "get successfully" });
        }
    }
}
