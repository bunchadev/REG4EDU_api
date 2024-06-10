namespace REG4EDU_api.Model.Dto
{
    public class SubjectSemesterDto
    {
        public Guid SemesterSubject_Id { get; set; }
        public Guid SemesterId { get; set; }
        public Guid SubjectId { get; set; }
        public string SemesterName { get; set; } = null!;
        public string SubjectName { get; set; } = null!;
        public string? SubjectCode { get; set; }
        public string? CreatedAt { get; set; }
    }
}
