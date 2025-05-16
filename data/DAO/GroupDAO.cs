using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.DAO
{
    public class GroupDAO
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public virtual IEnumerable<StudentsDAO> Students { get; set; }
        public virtual IEnumerable<GroupSubjectDAO> GroupSubjects { get; set; }
    }
}
