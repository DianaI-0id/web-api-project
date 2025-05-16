using data.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.Repository
{
    public interface IAttendanceRepository
    {
        IEnumerable<AttendanceDAO> GetAttendanceByGroupId( //сортировка по группе
            int groupId,
            DateOnly? date = null,
            int? lessonStart = null,
            int? lessonEnd = null,
            IEnumerable<StudentsDAO>? studentsList = null,
            int? subjectId = null,
            int? semester = null);

        bool GenerateAttendance(IEnumerable<AttendanceDAO> newAttendances); //генерация посещаемости
        bool DeleteAllRecords(); //очистить все записи
        bool DeleteAttendanceByGroupId(int groupId); //очистить записи по группе
        bool UpdateAttendance(AttendanceDAO attendanceDAO); //обновить посещаемость по параметрам
        bool CheckAttendanceExists(AttendanceDAO attendanceDAO); 
        //для обновления посещаемости нужно проверить наличие соответствующие записи в базе данных
    }
}
