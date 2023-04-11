namespace Kugel.PersonalSearch.Database.Entities;

public class Person
{
    public Guid Id { get; set; }
    public string PersonalId { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}