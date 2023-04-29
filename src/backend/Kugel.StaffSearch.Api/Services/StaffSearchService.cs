using Kugel.StaffSearch.Database.Entities;
using Kugel.StaffSearch.Database.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Kugel.StaffSearch.Api.Services;

internal class StaffSearchService : IStaffSearchService
{
    private readonly IStaffMemberRepository _staffMemberRepository;

    public StaffSearchService(IStaffMemberRepository staffMemberRepository)
    {
        _staffMemberRepository = staffMemberRepository;
    }
    public async Task<StaffMember[]?> Search(string query, int skip, int top)
    {
        return query == "*" 
            ? (await _staffMemberRepository.All())?.ToArray() 
            : (await _staffMemberRepository.Search(query, skip, top))?.ToArray();
    }

    public Task<StaffMember?> GetStaffMember(Guid id)
    {
        return _staffMemberRepository.Get(id);
    }
}