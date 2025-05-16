
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace console_ui
{
    public class GroupUI
    {
        //private readonly IGroupUseCase _groupService;

        ////конструктор
        //public GroupUI(IGroupUseCase groupService)
        //{
        //    _groupService = groupService;
        //}

        //public void addGroup() //добавляем группу через консоль, вызываем в классе Program.cs
        //{
        //    Console.WriteLine("Введите имя группы: ");
        //    var groupName = Console.ReadLine();
        //    _groupService.AddGroup(new AddGroupRequest { Name = groupName });
        //}

        //public void addGroupWithStudents()
        //{
        //    Console.WriteLine("Введите");
        //    var groupName = Console.ReadLine();

        //    AddGroupRequest addGroupRequest = new AddGroupRequest() { Name = groupName };
        //    List<AddStudentRequest> studentRequest = new List<AddStudentRequest>()
        //    {
        //        new AddStudentRequest {StudentName = "123"},
        //        new AddStudentRequest {StudentName = "234"},
        //        new AddStudentRequest {StudentName = "456"},
        //        new AddStudentRequest {StudentName = "678"}
        //    };

        //    AddGroupWithStudentsRequest addGroupWithStudentsRequest = new AddGroupWithStudentsRequest
        //    {
        //        addGroupRequest = addGroupRequest,
        //        AddStudentRequests = studentRequest
        //    };

        //    _groupService.AddGroupWithStudents(addGroupWithStudentsRequest);
        //}
    }
}
