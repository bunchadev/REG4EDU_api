namespace REG4EDU_api.Model.Dto.GetStudent
{
    public class GetStudentWithClassDto
    {
        public Guid SemesterId { get; set; }
        public Guid SubjectId { get; set; }
        public int ClassNumber { get; set; }
    }
}
