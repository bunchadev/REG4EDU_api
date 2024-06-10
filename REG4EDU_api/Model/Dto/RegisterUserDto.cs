namespace REG4EDU_api.Model.Dto
{
    public class RegisterUserDto
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; } = null!;
    }
}
