using Kugel.StaffSearch.Database.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Kugel.StaffSearch.Api.Services;

public interface IStaffSearchService
{
    Task<StaffMember[]?> Search(string query, int skip, int top);
    Task<StaffMember?> GetStaffMember(Guid id);
}