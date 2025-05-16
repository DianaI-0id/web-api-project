using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.DAO
{
    public class SubjectDAO
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }

        //связь с предметом и группой - можно например посмотреть какие группы изучают математику
        public virtual IEnumerable<GroupSubjectDAO> GroupSubjects { get; set; }
    }
}
