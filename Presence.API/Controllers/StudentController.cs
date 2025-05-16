using domain.Request;
using domain.Service;
using domain.UseCase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presence.API.Response;

namespace Presence.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        //сюда надо передать IGroupUseCase
        private readonly IStudentUseCase? _studentService;

        public StudentController(IStudentUseCase? studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("/students")]
        public ActionResult<StudentResponse> AddStudent([FromBody]AddStudentRequest request)
        {
            if (request == null)
                return BadRequest();

            var isAdded = _studentService.AddStudent(request);
            if (!isAdded)
                return StatusCode(500);

            return Ok();
        }

        [HttpGet("/students")]
        public ActionResult<IEnumerable<StudentResponse>> GetAllStudents()
        {
            var studentsList = _studentService.GetAllStudents().Select(student => 
                new StudentResponse
                {
                    Id = student.Id,
                    Name = student.Name,
                    //Group = new GroupResponse
                    //{ 
                    //    Id = student.Group.Id,
                    //    GroupName = student.Group.GroupName
                    //}
                });

            return Ok(studentsList);
        }
    }
}
