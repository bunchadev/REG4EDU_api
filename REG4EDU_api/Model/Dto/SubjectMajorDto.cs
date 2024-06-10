namespace REG4EDU_api.Model.Dto
{
    public class SubjectMajorDto
    {
        public Guid MajorsId { get; set; }
        public Guid SubjectId { get; set; }
        public string SubjectName { get; set; } = null!;
        public int NumberOfCredits { get; set; }
        public string? MajorName { get; set; }
        public string? SubjectCode { get; set; }
        public string? Category { get; set; }
        public string? CreatedAt { get; set; }
    }
}
