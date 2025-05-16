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
    public interface IGroupUseCase //тут будем передавать request и работа с entity
    {
        IEnumerable<GroupSubjectEntity> GetSubjectsByGroup(int groupId);
        IEnumerable<GroupEntity> GetStudentsByGroup(int groupId);
        bool AddGroupWithStudents(AddGroupWithStudentsRequest addGroupWithStudents);
        bool DeleteGroupById(int groupId);
        bool AddGroupForStudents(int groupId, List<int> studentsIds); //добавляем студентам группу
        bool AddGroupForSubjects(int groupId, List<int> subjectIds); //добавляем студентам группу

        IEnumerable<GroupEntity> GetAllGroups();
        bool AddGroup(AddGroupRequest addGroup);
        GroupEntity GetGroupById(int groupId);

        public IEnumerable<GroupEntity> GetGroupsWithStudents();
        Task<IEnumerable<GroupEntity>> GetGroupsWithStudentsAsync(); //все группы со студентами как Task
       
    }
}
