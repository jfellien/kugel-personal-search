using Kugel.StaffSearch.Database.Entities;

namespace Kugel.StaffSearch.Database.Repositories;

public interface IStaffMemberRepository
{
    Task<StaffMember?> Get(Guid id);
    Task<IEnumerable<StaffMember>?> Search(string queryString, int skip, int top);
    Task<IEnumerable<StaffMember>?> All();
}