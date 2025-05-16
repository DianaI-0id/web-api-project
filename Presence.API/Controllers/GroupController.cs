using domain.Entity;
using domain.Request;
using domain.UseCase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presence.API.Response;

namespace Presence.API.Controllers
    // /group/
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        //сюда надо передать IGroupUseCase
        private readonly IGroupUseCase? _groupService;

        public GroupController(IGroupUseCase? groupService)
        {
            _groupService = groupService;
        }

        //все группы
        [HttpGet("/group")]
        public ActionResult<IEnumerable<GroupResponse>> GetAllGroups()
        {
            var groups = _groupService.GetAllGroups()
                .Select(group => 
                    new GroupResponse
                    {
                        Id = group.Id,
                        GroupName = group.GroupName,
                        Students = group.Students?.Select(student =>
                           new StudentResponse
                           {
                               Id = student.Id,
                               Name = student.Name
                           })
                    });

            if (!groups.Any())
                return NoContent();

            return Ok(groups);
        }

        //Добавить группу вместе со студентами
        [HttpPost("/group/students")]
        public ActionResult AddGroupWithStudents([FromBody] AddGroupWithStudentsRequest addGroupWithStudents)
        {
            if (addGroupWithStudents == null)
            {
                return BadRequest("Пустой запрос");
            }

            bool result = _groupService.AddGroupWithStudents(addGroupWithStudents);
            if (result)
            {
                return CreatedAtAction(nameof(AddGroupWithStudents), new { groupName = addGroupWithStudents.addGroupRequest.GroupName }, addGroupWithStudents);
            }

            return StatusCode(500); //если не удалось добавить студента
        }

        //Получаем группу по id
        [HttpGet("/group/{groupId}")]
        public ActionResult<GroupResponse> GetGroupById(int groupId)
        {
            var group = _groupService.GetGroupById(groupId);
            if (group == null)
                return NotFound();

            var groupResponse = new GroupResponse
            {
                Id = group.Id,
                GroupName = group.GroupName,
                Students = group.Students?.Select(student =>
                    new StudentResponse
                    {
                        Id = student.Id,
                        Name = student.Name
                    })
            };

            return Ok(groupResponse);
        }

        [HttpGet("/group/{groupId}/students")] 
        public ActionResult<IEnumerable<GroupResponse>> GetStudentsByGroup(int groupId)
        {
            var studentsList = _groupService.GetStudentsByGroup(groupId).Select(group =>
                new GroupResponse
                {
                    Id = group.Id,
                    GroupName = group.GroupName,
                    Students = group.Students?.Select(student => new StudentResponse
                    {
                        Id = student.Id,
                        Name = student.Name
                    })
                });

            if (!studentsList.Any())
                return NotFound();

            return Ok(studentsList);
        }

        [HttpDelete("/group/{groupId}")]
        public ActionResult DeleteGroupById(int groupId)
        {
            bool result = _groupService.DeleteGroupById(groupId);
            if (result)
                return Ok(result);

            return BadRequest();
        }

        [HttpGet("/group/{groupId}/subjects")]
        public ActionResult<IEnumerable<SubjectResponse>> GetSubjectsByGroup(int groupId)
        {
            var subjects = _groupService.GetSubjectsByGroup(groupId);

            var response = subjects.Select(subject =>
                new SubjectResponse
                {
                    Id = subject.Subject.Id,
                    SubjectName = subject.Subject.SubjectName
                });

            return Ok(response);
        }

        //добавляем новую группу
        [HttpPost("/group")]
        public ActionResult AddGroup([FromBody]AddGroupRequest request)
        {
            if (request == null)
                return BadRequest();

            var isAdded = _groupService.AddGroup(request);
            if (!isAdded)
                return StatusCode(500);

            //return Ok();
            return CreatedAtAction(nameof(GetGroupById), new {GroupName = request.GroupName}, request);
        }
    }
}
