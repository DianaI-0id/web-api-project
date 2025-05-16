using data.DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using data.Repository;

namespace data.Repository
{
    //взаимодействие с базой данных через этот репозиторий
    public class SQLGroupRepository : IGroupRepository
    {
        private readonly RemoteDatabaseContext _dbContext;

        public SQLGroupRepository(RemoteDatabaseContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        public bool AddGroup(GroupDAO group)
        {
            _dbContext.Groups.Add(group);
            return _dbContext.SaveChanges() > 0;     
        }

        public IEnumerable<GroupDAO> GetAllGroups()
        {
            return _dbContext.Groups.Include(s => s.Students);
        }

        public GroupDAO GetGroupById(int groupId)
        {
            return _dbContext.Groups.Include(s => s.Students).FirstOrDefault(g => g.Id == groupId);
        }

        public IEnumerable<GroupSubjectDAO> GetSubjectsByGroup(int groupId)
        {
            return _dbContext.GroupSubjects.Include(s => s.Subject).Where(g => g.GroupId == groupId);
        }

        public IEnumerable<GroupDAO> GetStudentsByGroup(int groupId)
        {
            return _dbContext.Groups.Include(s => s.Students).Where(g => g.Id == groupId);
        }

        public bool AddGroupWithStudents(GroupDAO group, IEnumerable<StudentsDAO> students)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                _dbContext.Groups.Add(group); //добавили группу

                foreach (var student in students)
                {
                    student.Group = group; //указали группу для студента
                    _dbContext.Students.Add(student);
                }
                _dbContext.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
            }
            return false;
        }

        public bool DeleteGroupById(int groupId)
        {
            var group = _dbContext.Groups.FirstOrDefault(g => g.Id == groupId);
            _dbContext.Groups.Remove(group);
            return _dbContext.SaveChanges() > 0;
        }

        public bool AddGroupForStudents(int groupId, List<int> studentsIds)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                foreach (var itemId in studentsIds)
                {
                    var student = _dbContext.Students.FirstOrDefault(s => s.Id == itemId);
                    if (student != null) //если студент по Id нашелся
                    {
                        student.GroupId = groupId; //присваиваем студенту группу
                    }
                }
                _dbContext.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
            }
            return false;
        }

        public bool AddGroupForSubjects(int groupId, List<int> subjectIds)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                foreach (var itemId in subjectIds)
                {
                    var subject = _dbContext.Subjects.FirstOrDefault(s => s.Id == itemId); //берем все группы
                    if (subject != null) //если студент по Id нашелся
                    {
                        var groupsubject = new GroupSubjectDAO
                        {
                            GroupId = groupId,
                            SubjectId = subject.Id
                        };

                        _dbContext.GroupSubjects.Add(groupsubject);
                    }
                }
                _dbContext.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
            }
            return false;
        }

        public async Task<IEnumerable<GroupDAO>> GetGroupsWithStudentsAsync()
        {
            return await _dbContext.Groups.Include(g => g.Students).ToListAsync(); //без ListAsync не сработает
        }

        public async Task<IEnumerable<GroupDAO>> getAllGroupAsync()
        {
            return await _dbContext.Groups.Include(g => g.Students).ToListAsync();
        }
    }
}
