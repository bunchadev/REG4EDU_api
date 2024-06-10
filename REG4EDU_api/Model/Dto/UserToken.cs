using REG4EDU_api.Model.Domain;
using REG4EDU_api.Model.Dto.User;

namespace REG4EDU_api.Model.Dto
{
    public class UserToken
    {
        public string? Access_token { get; set; }
        public string? Refresh_token { get; set; }
        public User_Dto? User { get; set; }
    }
}
