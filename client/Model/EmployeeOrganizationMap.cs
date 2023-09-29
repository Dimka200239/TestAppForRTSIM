using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.Model
{
    public class EmployeeOrganizationMap
    {
        [DisplayName("LoginEmp")]
        [Required(ErrorMessage = "Требуется логин сотрудника")]
        public string loginEmp { get; set; }

        [DisplayName("OrgId")]
        [Required(ErrorMessage = "Требуется Id организации")]
        public int orgId { get; set; }
    }
}
