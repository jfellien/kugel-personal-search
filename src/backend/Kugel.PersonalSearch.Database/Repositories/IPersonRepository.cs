using Kugel.PersonalSearch.Database.Entities;

namespace Kugel.PersonalSearch.Database.Repositories;

public interface IPersonRepository
{
    Task<Person> Get(Guid id);
    Task<IEnumerable<Person>?> Search(string queryString);
}