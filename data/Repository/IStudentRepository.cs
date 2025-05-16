using data.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.Repository
{
    public interface IStudentRepository
    {
        IEnumerable<StudentsDAO> GetAllStudents();
        bool AddStudent(StudentsDAO student);
    }
}
