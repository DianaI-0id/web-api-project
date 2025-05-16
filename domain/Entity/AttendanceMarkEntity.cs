using data.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Entity
{
    public class AttendanceMarkEntity
    {
        public int Id { get; set; }
        public string MarkName { get; set; }

        // Навигационное свойство для связи с посещаемостью
        public IEnumerable<AttendanceEntity>? Attendances { get; set; }
    }
}
