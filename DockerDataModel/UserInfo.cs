using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DockerDataModel
{
    [Table("userinfo")]
    public class UserInfo
    {
        [Key]
        [Column("userid")]
        public string UserID { get; set; } = string.Empty;

        [Column("username")]
        public string UserName { get; set; } = string.Empty;
    }
}