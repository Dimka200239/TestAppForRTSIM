using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace server.Model
{
    public class Users
    {
        [DisplayName("LoginUser")]
        [Required(ErrorMessage = "Требуется логин пользователя")]
        [Key]
        public string loginUser { get; set; }

        [DisplayName("PasswordUser")]
        [Required(ErrorMessage = "Требуется пароль сотрудника")]
        public string passwordUser { get; set; }
    }
}
