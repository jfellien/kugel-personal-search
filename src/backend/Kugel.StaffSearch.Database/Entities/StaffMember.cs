namespace Kugel.StaffSearch.Database.Entities;

public class StaffMember
{
    public Guid Id { get; set; }
    public string PersonalId { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}