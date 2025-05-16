using data.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Entity
{
    public class SubjectEntity
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }

        //связь с предметом и группой - можно например посмотреть какие группы изучают математику
        public IEnumerable<GroupSubjectEntity> GroupSubjects { get; set; }
    }
}
