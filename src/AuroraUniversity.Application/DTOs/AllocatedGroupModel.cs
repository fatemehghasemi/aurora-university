namespace AuroraUniversity.Application.DTOs;

public class AllocatedGroupModel
{
    public Guid GroupId { get; set; }
    public string GroupName { get; set; } = string.Empty;
    public List<StudentModel> Students { get; set; } = new();
}
