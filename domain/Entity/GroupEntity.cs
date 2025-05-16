using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Entity
{
    public class GroupEntity
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public IEnumerable<StudentEntity>? Students { get; set; } = null;
        public IEnumerable<GroupSubjectEntity>? GroupSubjects { get; set; } // Связь с предметами
    }
}
