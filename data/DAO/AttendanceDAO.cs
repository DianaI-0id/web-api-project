using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.DAO
{
    public class AttendanceDAO
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public DateOnly AttendanceDate { get; set; }
        public int LessonA { get; set; } //начало диапазона уроков
        public int LessonB { get; set; } //конец диапазона уроков
        public int? AttendanceMarkId { get; set; } //отметка о посещаемости

        //навигационные свойства
        public virtual StudentsDAO Student { get; set; }
        public virtual SubjectDAO Subject { get; set; }
        public virtual AttendanceMarkDAO AttendanceMark { get; set; }
    }
}
