namespace REG4EDU_api.Model.Dto
{
    public class UserDto_1
    {
        public Guid UserId { get; set; }
        public string? Name { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string DepartmentName { get; set; } = null!;
    }
}
