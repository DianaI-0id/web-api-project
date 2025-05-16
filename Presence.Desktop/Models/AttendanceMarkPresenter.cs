using data.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presence.Desktop.Models
{
    public class AttendanceMarkPresenter
    {
        public int Id { get; set; }
        public string MarkName { get; set; }
        public IEnumerable<AttendancePresenter> Attendances { get; set; }
    }
}
