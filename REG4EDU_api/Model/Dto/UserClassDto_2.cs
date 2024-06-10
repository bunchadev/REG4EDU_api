namespace REG4EDU_api.Model.Dto
{
    public class UserClassDto_2
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int ClassNumber { get; set; }
        public string Classroom { get; set; } = null!;
        public int WeekDay { get; set; }
        public int OnShift { get; set; }
        public int EndShift { get; set; }
        public string? UserName { get; set; }
        public int Hours { get; set; } = 0;
        public string? Describe { get; set; } = null;
    }
}
