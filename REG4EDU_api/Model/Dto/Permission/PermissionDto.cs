namespace REG4EDU_api.Model.Dto.Permission
{
    public class PermissionDto
    {
        public Guid Id { get; set; }
        public string? PermissionName { get; set; }
        public string? ApiEndpoint { get; set; }
        public string? Description { get; set; }
        public string? UpdatedAt { get; set; }
    }
}
