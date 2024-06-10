namespace REG4EDU_api.Authentication
{
    public interface IJwtToken
    {
        string GenerateToken(Guid id, string role, int hours);
    }
}
