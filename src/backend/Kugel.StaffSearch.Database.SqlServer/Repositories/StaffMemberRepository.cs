using Kugel.StaffSearch.Database.Entities;
using Kugel.StaffSearch.Database.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kugel.StaffSearch.Database.SqlServer.Repositories;

public class StaffMemberRepository : IStaffMemberRepository
{
    private readonly KugelStaffSearchContext _dbContext;

    public StaffMemberRepository(KugelStaffSearchContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task<StaffMember> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<StaffMember>?> Search(string queryString, int skip, int top)
    {
        if (_dbContext.StaffMember == null) return null;
        
        return await _dbContext
            .StaffMember
            .Where(member => 
                member.FirstName.ToLower().Contains(queryString) 
                || member.LastName.ToLower().Contains(queryString)
                || member.PersonalId.ToLower().Contains(queryString))
            .Skip(skip)
            .Take(top)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<StaffMember>?> All()
    {
        if (_dbContext.StaffMember == null) return null;
        
        return await _dbContext.StaffMember.ToListAsync();
    }
}