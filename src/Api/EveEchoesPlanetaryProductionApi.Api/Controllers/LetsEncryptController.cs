namespace EveEchoesPlanetaryProductionApi.Api.Controllers
{
    using System.Threading.Tasks;

    using EveEchoesPlanetaryProductionApi.Data;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [ApiController]
    public class LetsEncryptController : ControllerBase
    {
        private readonly EveEchoesPlanetaryProductionApiDbContext dbContext;

        public LetsEncryptController(EveEchoesPlanetaryProductionApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [Route("~/.well-known/acme-challenge/{challenge}")]
        public async Task<IActionResult> Challenge(string challenge)
        {
            var response = await this.dbContext.Challenges.FirstOrDefaultAsync(x => x.Id.Equals(challenge));

            return this.Ok(response.Value);
        }
    }
}
