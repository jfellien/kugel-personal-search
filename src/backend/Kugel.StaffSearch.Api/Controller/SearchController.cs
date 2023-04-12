using Kugel.StaffSearch.Api.Services;
using Kugel.StaffSearch.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kugel.StaffSearch.Api.Controller;

[Authorize]
[ApiController]
[Route("search")]
public class SearchController : ControllerBase
{
    private readonly ILogger<SearchController> _logger;
    private readonly IStaffSearchService _staffSearchService;
    
    public SearchController(
        ILogger<SearchController> logger,
        IStaffSearchService staffSearchService)
    {
        _logger = logger;
        _staffSearchService = staffSearchService;

    }

    /// <summary>
    /// Search API that returns the queried StaffMember
    /// </summary>
    /// <param name="query">This parameter can can contain parts of FirstName, LastName or PersonalId</param>
    /// <param name="skip"></param>
    /// <param name="top"></param>
    /// <returns></returns>
    [HttpGet(Name = nameof(SearchForPeopleByQuery))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    public async Task<ActionResult<StaffMember[]>> SearchForPeopleByQuery(
        [FromQuery(Name = "q")] string query,
        [FromQuery(Name = "skip")] int skip,
        [FromQuery(Name = "top")] int top)
    {
        _logger.LogInformation("{0} started with query {1}", nameof(SearchForPeopleByQuery), query);

        if (string.IsNullOrEmpty(query) || top <= 0)
        {
            _logger.LogWarning("Wrong parameters for search query {0}, top {1}", 
                query,
                top);
            return BadRequest();
        }

        StaffMember[]? staffMembers = await _staffSearchService.Search(query, skip, top);
        
        return Ok(staffMembers);
    }
}
