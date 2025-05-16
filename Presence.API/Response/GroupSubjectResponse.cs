using domain.Entity;

namespace Presence.API.Response
{
    public class GroupSubjectResponse
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int SubjectId { get; set; }
        public int SemesterDuration { get; set; }
        public GroupResponse? Group { get; set; }
        public SubjectResponse? Subject { get; set; }
    }
}
