namespace REG4EDU_api.Model.Dto.User
{
    public class User_Dto
    {
        public Guid UserId { get; set; }
        public string? Name { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
