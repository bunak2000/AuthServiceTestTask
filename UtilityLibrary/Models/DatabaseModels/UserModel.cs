#region Imports
using System.ComponentModel.DataAnnotations.Schema;
#endregion

namespace UtilityLibrary.Models.DatabaseModels
{
    [Table("users")]
    public class UserModel
    {
        [Column("u_id")]
        public int Id { get; set; }
        [Column("u_login")]
        public string Login { get; set; } = string.Empty;
        [Column("u_password")]
        public string Password { get; set; } = string.Empty;
        [Column("u_details")]
        public string Details { get; set; } = string.Empty;
        [Column("u_register_date")]
        public DateTime RegisterDate { get; set; } = DateTime.MinValue;
    }
}
