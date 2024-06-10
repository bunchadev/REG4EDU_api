namespace REG4EDU_api.Model.Dto
{
    public class CreateSubject
    {
        public string SubjectName { get; set; } = null!;
        public int NumberOfCredits { get; set; }
        public string SubjectCode { get; set; } = "ABCDEF";
        public string? MajorsCode { get; set; }
        public string? Category { get; set; }
    }
}
