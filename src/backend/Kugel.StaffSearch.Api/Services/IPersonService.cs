using Kugel.StaffSearch.Database.Entities;

namespace Kugel.StaffSearch.Api.Services;

public interface IPersonService
{
    Task<StaffMember[]?> Search(string query, int skip, int top);
}