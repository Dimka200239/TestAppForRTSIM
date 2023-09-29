using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Model;
using server.Model.Data;

namespace server.Controller
{
    [Route("api/employeeOrganizationMapController")]
    [ApiController]
    public class EmployeeOrganizationMapController : ControllerBase
    {
        [HttpGet("getAllEmployeeInOrganization/{nameOrg}")]
        public async Task<ActionResult<List<Model.Employee>>> Get(string nameOrg)
        {
            List<Model.Employee> result = null;

            await using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                try
                {
                    var employees = db.Employees.Where(e => e.EmployeeOrganizationMaps
                        .Any(eom => eom.Organization.nameOrg == nameOrg))
                        .ToList();

                    result = employees;
                }
                catch (Exception ex)
                {
                    result = null;
                }
            }

            return result;
        }

        [HttpGet("getOrganizationByEmployeeLogin/{loginEmp}/{authTrue}")]
        public async Task<ActionResult<Model.Organization>> Get(string loginEmp, bool authTrue)
        {
            Model.Organization result = null;

            await using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                try
                {
                    var organization = db.Organizations.Where(o => o.EmployeeOrganizationMaps
                    .Any(eom => eom.Employee.loginEmp == loginEmp)).First();

                    result = organization;
                }
                catch (Exception ex)
                {
                    result = null;
                }
            }

            return result;
        }


        [HttpPost("addNewEmployeeOrganizationMap")]
        public async Task<ActionResult<bool>> Post(EmployeeOrganizationMap employeeOrganizationMap)
        {
            await using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                try
                {
                    await db.EmployeeOrganizationMaps.AddAsync(employeeOrganizationMap);

                    await db.SaveChangesAsync();

                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
        }
    }
}
