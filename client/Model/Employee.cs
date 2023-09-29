using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace client.Model
{
    public class Employee
    {
        [DisplayName("LoginEmp")]
        [Required(ErrorMessage = "Требуется логин сотрудника")]
        [Key]
        public string loginEmp { get; set; }

        [DisplayName("FisrstName")]
        [Required(ErrorMessage = "Требуется имя сотрудника")]
        public string fisrstName { get; set; }

        [DisplayName("MiddleName")]
        [Required(ErrorMessage = "Требуется фамилия сотрудника")]
        public string middleName { get; set; }

        [DisplayName("LastName")]
        [Required(ErrorMessage = "Требуется отчество сотрудника")]
        public string lastName { get; set; }

        [DisplayName("PasswordEmp")]
        [Required(ErrorMessage = "Требуется пароль сотрудника")]
        public string passwordEmp { get; set; }

        public List<EmployeeOrganizationMap> EmployeeOrganizationMaps { get; set; }
    }
}
