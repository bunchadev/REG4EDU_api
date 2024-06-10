namespace REG4EDU_api.Model.Dto
{
    public class CreateClassDto
    {
        public string Name { get; set; } = null!;
        public int ClassNumber { get; set; }
        public string Classroom { get; set; } = null!;
        public int WeekDay { get; set; }
        public int OnShift { get; set; }
        public int EndShift { get; set; }
        public int NumberStudent { get; set; }
        public string? Describe { get; set; } = null;
        public string? SemesterName { get; set; }
        public Guid SubjectId { get; set; }
    }
}
