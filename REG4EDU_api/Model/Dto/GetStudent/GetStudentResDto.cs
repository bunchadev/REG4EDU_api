namespace REG4EDU_api.Model.Dto.GetStudent
{
    public class GetStudentResDto
    {
        public Guid StudentId { get; set; }
        public string? UserName { get; set; } = null!;
        public string? MajorName { get; set; }
    }
}
