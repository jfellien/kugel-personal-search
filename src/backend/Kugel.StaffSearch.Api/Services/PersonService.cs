using Kugel.StaffSearch.Database.Entities;
using Kugel.StaffSearch.Database.Repositories;

namespace Kugel.StaffSearch.Api.Services;

internal class PersonService : IPersonService
{
    private readonly IStaffMemberRepository _staffMemberRepository;

    public PersonService(IStaffMemberRepository staffMemberRepository)
    {
        _staffMemberRepository = staffMemberRepository;
    }
    public async Task<StaffMember[]?> Search(string query, int skip, int top)
    {
        IEnumerable<StaffMember>? searchResult = await _staffMemberRepository.Search(query);

        if (searchResult is null) return null;
        return searchResult.Skip(skip).Take(top).ToArray();
    }
}