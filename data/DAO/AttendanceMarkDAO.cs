using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.DAO
{
    public class AttendanceMarkDAO //отметки посещаемости
    {
        public int Id { get; set; }
        public string MarkName { get; set; }

        // Навигационное свойство для связи с посещаемостью
        public virtual IEnumerable<AttendanceDAO> Attendances { get; set; }
    }
}
