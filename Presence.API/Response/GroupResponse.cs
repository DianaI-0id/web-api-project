using domain.Entity;

namespace Presence.API.Response
{
    public class GroupResponse
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public IEnumerable<StudentResponse>? Students { get; set; } = null;
        //public IEnumerable<GroupSubjectEntity>? GroupSubjects { get; set; } // Связь с предметами
    }
}
