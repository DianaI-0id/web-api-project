using data.DAO;
using data.Repository;
using domain.Entity;
using domain.Request;
using domain.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Service
{
    public class StudentService : IStudentUseCase
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository) //передаем репозиторий
        {
            _studentRepository = studentRepository;
        }
        public bool AddStudent(AddStudentRequest student)
        {
            return _studentRepository.AddStudent(new StudentsDAO { FullName = student.StudentName });
        }

        public IEnumerable<StudentEntity> GetAllStudents()
        {
            var studentsList = _studentRepository.GetAllStudents();

            return studentsList.Select(student =>
                new StudentEntity
                {
                    Id = student.Id,   
                    Name = student.FullName,
                    Group = new GroupEntity
                    {
                        Id = student.Group.Id,
                        GroupName = student.Group.GroupName
                    }
                });
        }
    }
}
