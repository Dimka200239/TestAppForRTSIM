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
        private readonly ApplicationContext _dbContext;

        public EmployeeController(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("getEmployeeByLogin/{loginEmp}")]
        public async Task<ActionResult<Model.Employee>> Get(string loginEmp)
        {
            Model.Employee result = null;

            await using (ApplicationContext db = _dbContext)
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

            await using (ApplicationContext db = _dbContext)
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
            await using (ApplicationContext db = _dbContext)
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
