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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace domain.Service
{
    public class AttendanceService : IAttendanceUseCase
    {
        private readonly IAttendanceRepository _attendanceRepository;
        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public bool DeleteAllRecords()
        {
            return _attendanceRepository.DeleteAllRecords();
        }

        public bool DeleteAttendanceByGroupId(int groupId)
        {
            return _attendanceRepository.DeleteAttendanceByGroupId(groupId);
        }

        public bool GenerateAttendance(List<GenerateAttendanceRequest> generateAttendanceRequests)
        {
            var daosList = generateAttendanceRequests.Select(a =>
                new AttendanceDAO
                {
                    StudentId = a.StudentId,
                    SubjectId = a.SubjectId,
                    LessonA = a.LessonStart,
                    LessonB = a.LessonEnd,
                    AttendanceDate = DateOnly.Parse(a.Date), //преобразуем строку в дату
                    AttendanceMarkId = 1 //по умолчанию студенты присутствуют на занятиях (id 1)
                });

            return _attendanceRepository.GenerateAttendance(daosList);
        }

        public IEnumerable<AttendanceEntity> GetAttendanceByGroupId(
            int groupId,
            DateOnly? date = null, 
            int? lessonStart = null,
            int? lessonEnd = null, 
            IEnumerable<StudentEntity>? studentsList = null, 
            int? subjectId = null, 
            int? semester = null)
        {
            var studentsDAO = studentsList?.Select(student =>
                new StudentsDAO
                {
                    Id = student.Id,
                    FullName = student.Name
                });

            var attendanceRecords = _attendanceRepository.GetAttendanceByGroupId( //тут получаем DAO
                groupId,
                date,
                lessonStart,
                lessonEnd,
                studentsDAO,
                subjectId,
                semester);

            //надо преобразовать в entity И вернуть в кач-ве результата
            return attendanceRecords.Select(dao =>
                new AttendanceEntity
                {
                    Id = dao.Id,
                    StudentId = dao.StudentId,
                    SubjectId = dao.SubjectId,
                    AttendanceDate = dao.AttendanceDate,
                    LessonA = dao.LessonA,
                    LessonB = dao.LessonB,
                    AttendanceMarkId = dao.AttendanceMarkId,
                    Subject = new SubjectEntity
                    {
                        Id = dao.Subject.Id,
                        SubjectName = dao.Subject.SubjectName
                    },
                    Student = new StudentEntity
                    {
                        Id = dao.Student.Id,
                        Name = dao.Student.FullName
                    },
                    AttendanceMark = new AttendanceMarkEntity
                    {
                        Id = dao.AttendanceMark.Id,
                        MarkName = dao.AttendanceMark.MarkName
                    }
                });
        }

        public bool UpdateAttendance(UpdateAttendanceDataRequest request)
        {
            var dataToUpdate = new AttendanceDAO
            {
                AttendanceDate = DateOnly.Parse(request.Date),
                LessonA = request.LessonStart,
                LessonB = request.LessonEnd,
                SubjectId = request.SubjectId,
                StudentId = request.StudentId,
                AttendanceMarkId = request.newAttendanceMarkId
            };

            return _attendanceRepository.UpdateAttendance(dataToUpdate);
        }

        public bool CheckAttendanceExists(UpdateAttendanceDataRequest request)
        {
            var attendance = new AttendanceDAO
            {
                AttendanceDate = DateOnly.Parse(request.Date),
                LessonA = request.LessonStart,
                LessonB = request.LessonEnd,
                //SubjectId = request.SubjectId, //subject тут не должно быть
                StudentId = request.StudentId
            };

            return _attendanceRepository.CheckAttendanceExists(attendance); //проверяем существование записи
        }
    }
}
