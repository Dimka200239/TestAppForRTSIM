using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Model
{
    public class EmployeeOrganizationMap
    {
        [DisplayName("LoginEmp")]
        [Required(ErrorMessage = "Требуется логин сотрудника")]
        [Key]
        public string loginEmp { get; set; }

        [DisplayName("OrgId")]
        [Required(ErrorMessage = "Требуется Id организации")]
        [Key]
        public int orgId { get; set; }

        [ForeignKey("loginEmp")]
        public Employee? Employee { get; set; }

        [ForeignKey("orgId")]
        public Organization? Organization { get; set; }
    }
}
