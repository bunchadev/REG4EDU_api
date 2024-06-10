namespace REG4EDU_api.Model.Dto
{
    public class UpdateClassDto
    {
        public Guid SubjectId { get; set; }
        public Guid SemesterId { get; set; }
        public Guid UserId { get; set; }
        public int ClassNumber { get; set; }
        public bool IsChecked { get; set; }
    }
}
