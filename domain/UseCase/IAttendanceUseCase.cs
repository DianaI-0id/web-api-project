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
    public interface IAttendanceUseCase
    {
        IEnumerable<AttendanceEntity> GetAttendanceByGroupId(
            int groupId,
            DateOnly? date = null,
            int? lessonStart = null,
            int? lessonEnd = null,
            IEnumerable<StudentEntity>? studentsList = null,
            int? subjectId = null,
            int? semester = null);

        bool DeleteAllRecords();
        bool GenerateAttendance(List<GenerateAttendanceRequest> generateAttendanceRequests); //генерация посещаемости
        bool DeleteAttendanceByGroupId(int groupId); //удалить посещаемость по группе
        bool UpdateAttendance(UpdateAttendanceDataRequest request); //передаем параметры для обновления
        bool CheckAttendanceExists(UpdateAttendanceDataRequest request); //проверить существование студента
    }
}
