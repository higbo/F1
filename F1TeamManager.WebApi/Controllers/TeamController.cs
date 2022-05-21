namespace F1TeamManager.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using F1.Common.DataContext;
    using F1TeamManager.WebApi.Repositories;
    using F1.Common.DataContext.DataModel;

    // base address: api/teams
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamRepository repo;

        public TeamController(ITeamRepository repo)
        {
            this.repo = repo;
        }

        // GET: api/Team
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Team>))]
        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await repo.RetrieveAllAsync();
        }

        // GET: api/team/[id]
        [HttpGet("{id}", Name = nameof(GetTeam))]
        [ProducesResponseType(200, Type = typeof(Team))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetTeam(int id)
        {
            Team? t = await repo.RetrieveAsync(id);
            if (t == null)
            {
                return NotFound();
            }
            return Ok(t);
        }

        // POST: api/team
        // BODY: Team (JSON, XML)
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Team))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] Team t)
        {
            if (t == null)
            {
                return BadRequest();
            }

            Team? addedTeam = await repo.CreateAsync(t);

            if (addedTeam == null)
            {
                return BadRequest("Repository failed to create F1 Team.");
            }
            else
            {
                return CreatedAtRoute(
                  routeName: nameof(GetTeam),
                  routeValues: new { id = addedTeam.Id },
                  value: addedTeam);
            }
        }

        // PUT: api/teams/[id]
        // BODY: Team (JSON, XML)
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(
          int id, [FromBody] Team t)
        {

            if (t == null || t.Id != id)
            {
                return BadRequest(); // 400 Bad request
            }

            Team? existing = await repo.RetrieveAsync(id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }

            await repo.UpdateAsync(id, t);

            return new NoContentResult(); // 204 No content
        }

        // DELETE: api/team/[id]
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {

            Team? existing = await repo.RetrieveAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            bool? deleted = await repo.DeleteAsync(id);

            if (deleted.HasValue && deleted.Value)
            {
                return new NoContentResult();
            }
            else
            {
                return BadRequest(
                  $"Team {id} was found but failed to delete.");
            }
        }
    }
}
