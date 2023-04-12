using Kugel.PersonalSearch.Api.Services;
using Kugel.PersonalSearch.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kugel.PersonalSearch.Api.Controller;

[Authorize]
[ApiController]
[Route("search")]
public class SearchController : ControllerBase
{
    private readonly ILogger<SearchController> _logger;
    private readonly IPersonService _personService;
    
    public SearchController(
        ILogger<SearchController> logger,
        IPersonService personService)
    {
        _logger = logger;
        _personService = personService;

    }

    /// <summary>
    /// Search API that returns the queried People
    /// </summary>
    /// <param name="query">This parameter can can contain parts of FirstName, LastName or PersonalId</param>
    /// <param name="skip"></param>
    /// <param name="top"></param>
    /// <returns></returns>
    [HttpGet(Name = nameof(SearchForPeopleByQuery))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("application/json")]
    public async Task<ActionResult<Person[]>> SearchForPeopleByQuery(
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

        Person[]? people = await _personService.Search(query, skip, top);
        
        return Ok(people);
    }
}
