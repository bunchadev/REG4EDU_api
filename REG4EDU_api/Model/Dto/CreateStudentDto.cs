namespace REG4EDU_api.Model.Dto
{
    public class CreateStudentDto
    {
        public string? UserName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Password { get; set; } = null!;
        public string? MajorsCode { get; set; }
    }
}
