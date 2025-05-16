using domain.Request;
using domain.UseCase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presence.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IGroupUseCase _groupService;

        public AdminController(IGroupUseCase groupService)
        {
            _groupService = groupService;
        }

        [HttpPost("/admin/{groupId}/students")] //добавляем группу студентам
        public ActionResult AddGroupForStudents(int groupId, [FromBody] StudentIdsRequest request)
        {
            bool result = _groupService.AddGroupForStudents(groupId, request.StudentIds);
            return Ok(result);
        }

        [HttpPost("/admin/{groupId}/subjects")] //добавляем группу предметам
        public ActionResult AddGroupForSubjects(int groupId, [FromBody] SubjectIdsRequest request)
        {
            bool result = _groupService.AddGroupForSubjects(groupId, request.SubjectIds);
            return Ok(result);
        }
    }
}
