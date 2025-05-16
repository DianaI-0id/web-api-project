using domain.Entity;

namespace Presence.API.Response
{
    public class AttendanceResponse
    {
        public int Id { get; set; }
        //public int StudentId { get; set; }
        //public int? SubjectId { get; set; }
        public string? AttendanceDate { get; set; }
        public int? LessonA { get; set; } //начало диапазона уроков
        public int? LessonB { get; set; } //конец диапазона уроков
        //public int? AttendanceMarkId { get; set; } //отметка о посещаемости
        public int? SemesterDuration { get; set; }

        //навигационные свойства
        public StudentResponse? Student { get; set; }
        public SubjectResponse? Subject { get; set; }
        public AttendanceMarkResponse? AttendanceMark { get; set; }
    }
}
