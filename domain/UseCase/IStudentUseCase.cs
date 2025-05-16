using data.DAO;
using domain.Entity;
using domain.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.UseCase
{
    public interface IStudentUseCase
    {
        IEnumerable<StudentEntity> GetAllStudents();
        bool AddStudent(AddStudentRequest student);
    }
}
