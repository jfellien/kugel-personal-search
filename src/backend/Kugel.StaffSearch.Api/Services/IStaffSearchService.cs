using Kugel.StaffSearch.Database.Entities;

namespace Kugel.StaffSearch.Api.Services;

public interface IStaffSearchService
{
    Task<StaffMember[]?> Search(string query, int skip, int top);
}