using data.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presence.Desktop.Models
{
    public class GroupSubjectPresenter
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int SubjectId { get; set; }
        public int? SemesterDuration { get; set; }

        public GroupPresenter Group { get; set; }
        public SubjectPresenter Subject { get; set; }
    }
}
