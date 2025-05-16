using data.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.Repository
{
    public interface IGroupRepository
    {
        IEnumerable<GroupSubjectDAO> GetSubjectsByGroup(int groupId);
        bool DeleteGroupById(int groupId);
        bool AddGroupForStudents(int groupId, List<int> studentsIds); //добавляем студентам группу
        bool AddGroupWithStudents(GroupDAO group, IEnumerable<StudentsDAO> students);
        bool AddGroupForSubjects(int groupId, List<int> subjectIds); //для группы добавляем список предметов

        //вывести группу вместе со студентами
        IEnumerable<GroupDAO> GetStudentsByGroup(int groupId);
        IEnumerable<GroupDAO> GetAllGroups(); //все группы
        public Task<IEnumerable<GroupDAO>> getAllGroupAsync(); //все группы как TASK
        GroupDAO GetGroupById(int groupId);
        bool AddGroup(GroupDAO group);

        public Task<IEnumerable<GroupDAO>> GetGroupsWithStudentsAsync(); //все группы со студентами в Desktop
    }
}
