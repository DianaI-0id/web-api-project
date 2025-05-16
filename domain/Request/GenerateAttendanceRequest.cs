using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Request
{
    public class GenerateAttendanceRequest
    {
        public int LessonStart { get; set; }
        public int LessonEnd { get; set; }
        public int SubjectId {  get; set; }
        public string Date {  get; set; }
        public int StudentId {  get; set; }
    }
}
