using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Model.Data;

namespace server.Controller
{
    [Route("api/organizationController")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly ApplicationContext _dbContext;

        public OrganizationController(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("getAllOrganizations")]
        public async Task<ActionResult<List<Model.Organization>>> Get()
        {
            List<Model.Organization> result = null;

            await using (ApplicationContext db = _dbContext)
            {
                try
                {
                    var organization = db.Organizations.ToList();

                    result = organization;
                }
                catch (Exception ex)
                {
                    result = null;
                }
            }

            return result;
        }

        [HttpGet("getOrganizationByName/{name}")]
        public async Task<ActionResult<Model.Organization>> Get(string name)
        {
            Model.Organization result = null;

            await using (ApplicationContext db = _dbContext)
            {
                try
                {
                    var organization = db.Organizations.FirstOrDefault(x => x.nameOrg == name);

                    result = organization;
                }
                catch (Exception ex)
                {
                    result = null;
                }
            }

            return result;
        }
    }
}
