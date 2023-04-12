using Kugel.PersonalSearch.Database.Entities;
using Kugel.PersonalSearch.Database.Repositories;

namespace Kugel.PersonalSearch.Api.Services;

internal class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }
    public async Task<Person[]?> Search(string query, int skip, int top)
    {
        IEnumerable<Person>? searchResult = await _personRepository.Search(query);

        return searchResult?.Skip(skip).Take(top).ToArray();
    }
}