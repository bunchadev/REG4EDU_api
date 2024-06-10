namespace REG4EDU_api.Model.Dto.Permission
{
    public class UpdatePermissionDto
    {
        public Guid Id { get; set; }
        public string? PermissionName { get; set; }
        public string? ApiEndpoint { get; set; }
        public string? Description { get; set; }
    }
}
