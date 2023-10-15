using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace server.Model
{
    public class Gender
    {
        [DisplayName("Abbreviation")]
        [Required(ErrorMessage = "Требуется сокращенное написание гендера")]
        [Key]
        public string abbreviation { get; set; }

        [DisplayName("Meaning")]
        [Required(ErrorMessage = "Требуется расшифровка сокращения")]
        public string meaning { get; set; }

        public List<Employee>? Employees { get; set; }
    }
}
