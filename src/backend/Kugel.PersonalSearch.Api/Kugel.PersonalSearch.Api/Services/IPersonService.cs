using Kugel.PersonalSearch.Database.Entities;

namespace Kugel.PersonalSearch.Api.Services;

public interface IPersonService
{
    Task<Person[]?> Search(string query, int skip, int top);
}