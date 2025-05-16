using data.DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace data.Repository
{
    public class SQLAttendanceRepository : IAttendanceRepository
    {
        private readonly RemoteDatabaseContext _dbContext;

        public SQLAttendanceRepository(RemoteDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CheckAttendanceExists(AttendanceDAO attendanceDAO) //проверяем существует ли пользователь
        {
            return _dbContext.Attendances.Contains(attendanceDAO);
        }

        public bool DeleteAllRecords()
        {
            foreach (var attendance in _dbContext.Attendances)
            {
                _dbContext.Attendances.Remove(attendance);
            }

            _dbContext.SaveChanges();
            return true;
        }

        public bool DeleteAttendanceByGroupId(int groupId)
        {
            var attendances = _dbContext.Attendances
                .Include(g => g.Student)
                .ThenInclude(g => g.Group)
                .Where(s => s.Student.GroupId == groupId);

            _dbContext.Attendances.RemoveRange(attendances);
            return _dbContext.SaveChanges() > 0;
        }

        public bool GenerateAttendance(IEnumerable<AttendanceDAO> newAttendances) //добавляем переданный массив объектов atteendance
        {
            foreach (var item in newAttendances)
            {
                _dbContext.Attendances.Add(item);
            }     
            return _dbContext.SaveChanges() > 0;
        }

        //выводим список предварительно профильтровав
        public IEnumerable<AttendanceDAO> GetAttendanceByGroupId(int groupId, DateOnly? date = null, int? lessonStart = null, int? lessonEnd = null, IEnumerable<StudentsDAO>? studentsList = null, int? subjectId = null, int? semester = null)
        {
            var allRecords = _dbContext
                .Attendances
                .Include(s => s.Subject)
                .Include(s => s.Student)
                .Include(a => a.AttendanceMark)
                .Where(g => g.Student.GroupId == groupId);

            //фильтрация по дате
            if (date.HasValue)
                allRecords = allRecords.Where(d => d.AttendanceDate == date);

            //фильтрация по диапазону уроков
            if (lessonStart.HasValue && lessonEnd.HasValue)
                allRecords = allRecords.Where(a => a.LessonA >=  lessonStart.Value && a.LessonB <= lessonEnd);

            //Фильтрация по списку студентов
            if (studentsList != null && studentsList.Any())
            {
                var studentsIds = studentsList.Select(s => s.Id); //берем id всех студентов
                allRecords = allRecords.Where(a => studentsIds.Contains(a.StudentId));
            }

            //Фильтрация по предмету
            if (subjectId.HasValue)
                allRecords = allRecords.Where(s => s.SubjectId == subjectId);

            //Фильтрация по семестру
            if (semester.HasValue)
            {
                var subjectIdsForSemester = _dbContext
                    .GroupSubjects
                    .Where(gs => gs.GroupId == groupId && gs.SemesterDuration == semester.Value)
                    .Select(gs => gs.SubjectId);

                allRecords = allRecords.Where(g => subjectIdsForSemester.Contains(g.SubjectId));
            }

            return allRecords;
        }

        public bool UpdateAttendance(AttendanceDAO attendanceDAO)
        {
            _dbContext.Update(attendanceDAO);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
