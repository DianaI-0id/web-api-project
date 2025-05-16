using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace data.DAO
{
    public class StudentsDAO
    {
        public int Id { get; set; } //идентификатор
        public string FullName { get; set; } //ФИО
        public int? GroupId { get; set; } //ID группы

        //связь с группой
        public virtual GroupDAO Group { get; set; }
    }
}
