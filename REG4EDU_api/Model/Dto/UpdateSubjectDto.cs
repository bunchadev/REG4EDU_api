namespace REG4EDU_api.Model.Dto
{
    public class UpdateSubjectDto
    {
        public Guid MajorsId { get; set; }
        public Guid SubjectId { get; set; }
        public string SubjectName { get; set; } = null!;
        public int NumberOfCredits { get; set; }
        public string? MajorName { get; set; } = null;
        public string? SubjectCode { get; set; }
        public bool Check1 { get; set; } = false;
        public bool Check2 { get; set; } = false;
        public string? Category { get; set; } = null;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
