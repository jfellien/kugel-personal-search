using Kugel.PersonalSearch.Database.Entities;
using Kugel.PersonalSearch.Database.Repositories;

namespace Kugel.PersonalSearch.Database.SqlServer.Repositories;

public class PersonRepository : IPersonRepository
{
    public Task<Person> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Person>> Search(string queryString)
    {
        throw new NotImplementedException();
    }
}