using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Model;
using server.Model.Data;
using System.Threading.Tasks;

namespace server.Controller
{
    [Route("api/employeeController")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpGet("getEmployeeByLogin/{loginEmp}")]
        public async Task<ActionResult<Model.Employee>> Get(string loginEmp)
        {
            Model.Employee result = null;

            await using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                try
                {
                    var employee = db.Employees.FirstOrDefault(x => x.loginEmp == loginEmp);

                    result = employee;
                }
                catch (Exception ex)
                {
                    result = null;
                }
            }

            return result;
        }

        [HttpGet("getEmployeeByLoginAndPassword/{loginEmp}/{passworEmp}")]
        public async Task<ActionResult<Model.Employee>> Get(string loginEmp, string passworEmp)
        {
            Model.Employee result = null;

            await using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                try
                {
                    var employee = db.Employees.FirstOrDefault(x => x.loginEmp == loginEmp && x.passwordEmp == passworEmp);

                    result = employee;
                }
                catch (Exception ex)
                {
                    result = null;
                }
            }

            return result;
        }

        [HttpPost("addNewEmployee")]
        public async Task<ActionResult<bool>> Post(Employee employee)
        {
            await using (ApplicationContext db = new ApplicationContext(new DbContextOptions<ApplicationContext>()))
            {
                try
                {
                    await db.Employees.AddAsync(employee);

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
