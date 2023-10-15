using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Model
{
    public class Comments
    {
        [DisplayName("CommentId")]
        [Key]
        public string commentId { get; set; }

        [DisplayName("LoginUser")]
        public string loginUser { get; set; }

        [ForeignKey("loginUser")]
        public Users? Users { get; set; }
    }
}
