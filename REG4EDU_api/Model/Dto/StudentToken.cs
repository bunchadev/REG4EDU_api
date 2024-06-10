using REG4EDU_api.Model.Domain;

namespace REG4EDU_api.Model.Dto
{
    public class StudentToken
    {
        public string? Access_token { get; set; }
        public string? Refresh_token { get; set; }
        public Student? User { get; set; }
    }
}
