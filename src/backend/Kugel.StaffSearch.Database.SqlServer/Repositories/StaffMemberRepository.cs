using Kugel.StaffSearch.Database.Entities;
using Kugel.StaffSearch.Database.Repositories;

namespace Kugel.StaffSearch.Database.SqlServer.Repositories;

public class StaffMemberRepository : IStaffMemberRepository
{
    public Task<StaffMember> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<StaffMember>?> Search(string queryString)
    {
        return null;
    }
}