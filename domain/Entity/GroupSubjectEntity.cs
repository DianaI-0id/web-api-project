using data.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Entity
{
    public class GroupSubjectEntity
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int SubjectId { get; set; }
        public int SemesterDuration { get; set; }

        //связь с группой и предметом
        public GroupEntity Group { get; set; }
        public SubjectEntity Subject { get; set; }
    }
}
