using Kugel.StaffSearch.Api.Services;
using Kugel.StaffSearch.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kugel.StaffSearch.Api.Controller;

[Authorize]
[ApiController]
[Route("staff-members/{id}")]
public class StaffMemberController : ControllerBase
{
    private readonly ILogger<StaffMemberController> _logger;
    private readonly IStaffSearchService _staffSearchService;
    
    public StaffMemberController(
        ILogger<StaffMemberController> logger,
        IStaffSearchService staffSearchService)
    {
        _logger = logger;
        _staffSearchService = staffSearchService;
    }

    /// <summary>
    /// Gets a StaffMember by its Id
    /// </summary>
    /// <param name="id">Id of the Staff Member</param>
    /// <returns></returns>
    [HttpGet(Name = nameof(GetStaffMember))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    public async Task<ActionResult<StaffMember[]>> GetStaffMember(string id)
    {
        if (Guid.TryParse(id, out Guid staffMemberId) == false )
        {
            return BadRequest("Invalid id. Guid/UUID expected");
        }
        
        try
        {
            StaffMember? staffMember = await _staffSearchService.GetStaffMember(staffMemberId);

            return Ok(staffMember);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}