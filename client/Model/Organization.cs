using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client.Model
{
    public class Organization
    {
        [DisplayName("OrgId")]
        [Required(ErrorMessage = "Требуется Id организации")]
        public int orgId { get; set; }

        [DisplayName("NameOrg")]
        [Required(ErrorMessage = "Требуется название организации")]
        public string nameOrg { get; set; }
    }
}
