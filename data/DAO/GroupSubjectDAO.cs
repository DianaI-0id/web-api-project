using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace data.DAO
{
    public class GroupSubjectDAO
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int SubjectId { get; set; }
        public int? SemesterDuration { get; set; } //это поле пока что может быть пустым

        //связь с группой и предметом
        public virtual GroupDAO Group { get; set; }
        public virtual SubjectDAO Subject { get; set; }

    }
}
