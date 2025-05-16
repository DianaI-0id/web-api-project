using data.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Entity
{
    public class AttendanceEntity
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public DateOnly AttendanceDate { get; set; }
        public int LessonA { get; set; } //начало диапазона уроков
        public int LessonB { get; set; } //конец диапазона уроков
        public int? AttendanceMarkId { get; set; } //отметка о посещаемости
        public int? SemesterDuration { get; set; }

        //навигационные свойства
        public StudentEntity? Student { get; set; }
        public SubjectEntity? Subject { get; set; }
        public AttendanceMarkEntity? AttendanceMark { get; set; }
    }
}
