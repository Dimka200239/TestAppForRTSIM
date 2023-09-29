using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace server.Model
{
    public class Organization
    {
        [DisplayName("OrgId")]
        [Required(ErrorMessage = "Требуется Id организации")]
        [Key]
        public int orgId { get; set; }

        [DisplayName("NameOrg")]
        [Required(ErrorMessage = "Требуется название организации")]
        public string nameOrg { get; set; }

        public List<EmployeeOrganizationMap>? EmployeeOrganizationMaps { get; set; }
    }
}
