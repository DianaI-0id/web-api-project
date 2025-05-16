using domain.Entity;
using domain.Request;
using domain.UseCase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presence.API.Response;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Presence.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceUseCase _attendanceService;
        public AttendanceController(IAttendanceUseCase attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet("/attendance/{groupId}/")]
        public ActionResult<IEnumerable<AttendanceResponse>> GetAttendancesByGroupId(
            int groupId,
            string? date = null, //передаем дату как строку сначала
            int? lessonStart = null,
            int? lessonEnd = null,
            [FromQuery] StudentIdsRequest? request = null,
            int? subjectId = null,
            int? semester = null
            )
        {
            DateOnly? attendanceDate = null;

            if (!string.IsNullOrEmpty(date))
            {
                // Преобразуем строку даты в DateOnly
                if (DateOnly.TryParse(date, out var parsedDate))
                {
                    attendanceDate = parsedDate;
                }
                else
                {
                    return BadRequest("Invalid date format. Please use YYYY-MM-DD.");
                }
            }

            //преобразовали все Id в студентов
            IEnumerable<StudentEntity>? studentsList = request?.StudentIds?.Select(id =>
                new StudentEntity
                {
                    Id = id
                });

            // Получаем записи о посещаемости из сервиса
            var attendanceRecords = _attendanceService.GetAttendanceByGroupId(
                groupId,
                attendanceDate,
                lessonStart,
                lessonEnd,
                studentsList,
                subjectId,
                semester);

            var response = attendanceRecords.Select(attendance =>
                new AttendanceResponse
                {
                    Id = attendance.Id,
                    Subject = attendance.Subject != null ? new SubjectResponse
                    {
                        Id = attendance.SubjectId,
                        SubjectName = attendance.Subject.SubjectName
                    } : null,
                    Student = attendance.Student != null ? new StudentResponse
                    {
                        Id = attendance.StudentId,
                        Name = attendance.Student.Name
                    } : null,
                    LessonA = attendance.LessonA,
                    LessonB = attendance.LessonB,
                    AttendanceDate = attendanceDate.ToString(),
                    SemesterDuration = attendance.Subject != null &&
                                       attendance.Subject.GroupSubjects != null ? attendance.Subject
                                       .GroupSubjects
                                       .Where(gs => gs.GroupId == groupId && gs.SubjectId == subjectId)
                                       .Select(s => s.SemesterDuration)
                                       .FirstOrDefault()
                                    : null,

                    AttendanceMark = attendance.AttendanceMark != null ? new AttendanceMarkResponse
                    {
                        Id = attendance.AttendanceMark.Id,
                        MarkName = attendance.AttendanceMark.MarkName
                    } : null
                });

            return Ok(response);
        }

        [HttpPost("/attendance")]
        public ActionResult GenerateAttendance([FromBody] List<GenerateAttendanceRequest> request)
        {
            // Проверка и преобразование даты
            foreach (var attendanceRequest in request)
            {
                if (!DateOnly.TryParse(attendanceRequest.Date, out var dateOnly))
                {
                    return BadRequest($"Invalid date format for date: {attendanceRequest.Date}. Need YYYY-MM-DD");
                }
            }
            return Ok(_attendanceService.GenerateAttendance(request));
        }

        [HttpDelete("/attendance")]
        public ActionResult<bool> DeleteAllRecords()
        {
            return Ok(_attendanceService.DeleteAllRecords());
        }

        [HttpDelete("/attendances/{groupId}")]
        public ActionResult DeleteAttendancesByGroupId(int groupId)
        {
            return Ok(_attendanceService.DeleteAttendanceByGroupId(groupId));
        }

        //ДОБАВИТЬ PUT ATTENDANCE(EDIT)
        [HttpPut("/attendance")]
        public ActionResult UpdateAttendance([FromBody]UpdateAttendanceDataRequest request)
        {
            if (!DateOnly.TryParse(request.Date, out var dateOnly))
            {
                return BadRequest($"Invalid date format for date: {request.Date}. Need YYYY-MM-DD");
            }

            var attendanceExists = _attendanceService.CheckAttendanceExists(request);
            //добавить проверку существует ли пользователь в БД
            //если нет то вывести bad request
            //иначе обновить введенные данные о посещаемости
            
            return Ok();
        }
    }
}
