using data.DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.Repository
{
    public class SQLStudentRepository : IStudentRepository
    {
        private readonly RemoteDatabaseContext _dbContext;
        public SQLStudentRepository(RemoteDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddStudent(StudentsDAO student)
        {
            _dbContext.Students.Add(student);
            return _dbContext.SaveChanges() > 0;
        }

        public IEnumerable<StudentsDAO> GetAllStudents()
        {
            return _dbContext.Students.Include(g => g.Group);
        }
    }
}
