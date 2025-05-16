using data.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presence.Desktop.Models
{
    public class AttendancePresenter
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public DateOnly AttendanceDate { get; set; }
        public int LessonA { get; set; } //начало диапазона уроков
        public int LessonB { get; set; } //конец диапазона уроков
        public int? AttendanceMarkId { get; set; } //отметка о посещаемости

        //навигационные свойства
        public StudentPresenter Student { get; set; }
        public SubjectPresenter Subject { get; set; }
        public AttendanceMarkDAO AttendanceMark { get; set; }
    }
}
