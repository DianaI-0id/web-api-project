using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Request
{
    public class AddGroupWithStudentsRequest
    {
        public AddGroupRequest addGroupRequest {  get; set; }
        public List<AddStudentRequest> addStudentRequests { get; set; } //ошибка с ienumerable при обработке нескольких студентов - поставить list
    }
}
