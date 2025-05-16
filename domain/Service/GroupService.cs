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
    public class GroupService : IGroupUseCase
    {
        private readonly IGroupRepository _groupRepository;
        public GroupService(IGroupRepository groupRepository) //передаем репозиторий
        { 
            _groupRepository = groupRepository;
        }
        public bool AddGroup(AddGroupRequest addGroup) //при автогенерации ключа Id не нужен
        {
            return _groupRepository.AddGroup(new GroupDAO { GroupName = addGroup.GroupName });
            //return true; //проверить как это работает
        }

        public bool AddGroupWithStudents(AddGroupWithStudentsRequest addGroupWithStudents)
        {
            GroupDAO group = new GroupDAO
            {
                GroupName = addGroupWithStudents.addGroupRequest.GroupName
            };

            List<StudentsDAO> studentsList = addGroupWithStudents
                .addStudentRequests
                .Select(it => new StudentsDAO
                {
                    FullName = it.StudentName

                }).ToList();

            return _groupRepository.AddGroupWithStudents(group, studentsList);
        }

        public bool AddGroupForStudents(int groupId, List<int> studentsIds)
        {
            return _groupRepository.AddGroupForStudents(groupId, studentsIds);
        }

        public bool DeleteGroupById(int groupId) //удаляем группу по указанному id
        {
            return _groupRepository.DeleteGroupById(groupId);
        }

        public IEnumerable<GroupEntity> GetAllGroups()
        {
            var groupsList = _groupRepository.GetAllGroups();

            return groupsList.Select(group => new GroupEntity
            {
                Id = group.Id,
                GroupName = group.GroupName,
                Students = group.Students.Select(student =>
                    new StudentEntity
                    {
                        Id = student.Id,
                        Name = student.FullName
                    })
            });
        }

        public GroupEntity GetGroupById(int groupId)
        {
            var group = _groupRepository.GetGroupById(groupId);
            return new GroupEntity
            {
                Id = group.Id,
                GroupName = group.GroupName,
                Students = group.Students?.Select(student => 
                    new StudentEntity
                    {
                        Id = student.Id,
                        Name = student.FullName
                    })
            };
        }

        public IEnumerable<GroupEntity> GetStudentsByGroup(int groupId)
        {
            var studentsList = _groupRepository.GetStudentsByGroup(groupId).Select(group =>
                new GroupEntity
                {
                    Id = group.Id,
                    GroupName = group.GroupName,
                    Students = group.Students?.Select(student => new StudentEntity
                    {
                        Id = student.Id,
                        Name = student.FullName
                    })
                });

            return studentsList;
        }

        public IEnumerable<GroupSubjectEntity> GetSubjectsByGroup(int groupId)
        {
            var group = _groupRepository.GetGroupById(groupId);
            var subjects = _groupRepository.GetSubjectsByGroup(groupId);

            return subjects.Select(group =>
                new GroupSubjectEntity
                {
                    Id = group.Id,
                    GroupId = group.Id,
                    Subject = new SubjectEntity
                    {
                        Id = group.SubjectId,
                        SubjectName = group.Subject.SubjectName
                    }
                });
        }

        public bool AddGroupForSubjects(int groupId, List<int> subjectIds)
        {
            return _groupRepository.AddGroupForSubjects(groupId, subjectIds);
        }

        public IEnumerable<GroupEntity> GetGroupsWithStudents()
        {
            return _groupRepository.GetAllGroups().Select(
                group => new GroupEntity
                {
                    Id = group.Id,
                    GroupName = group.GroupName,
                    Students = group.Students?.Select(
                        user => new StudentEntity
                        {
                            Id = user.Id,
                            Name = user.FullName,
                            Group = new GroupEntity
                            {
                                Id = group.Id,
                                GroupName = group.GroupName,
                            }
                        }).ToList()
                }).ToList();
        }

        public async Task<IEnumerable<GroupEntity>> GetGroupsWithStudentsAsync()
        {
            var result = await _groupRepository.getAllGroupAsync();

            return result.Select(
                group => new GroupEntity
                {
                    Id = group.Id,
                    GroupName = group.GroupName,
                    Students = group.Students.Select(
                        user => new StudentEntity
                        {
                            Id = user.Id,
                            Name = user.FullName,
                            Group = new GroupEntity
                            {
                                Id = group.Id,
                                GroupName = group.GroupName,
                            }
                        }).ToList()
                }).ToList();
        }
    }
}
