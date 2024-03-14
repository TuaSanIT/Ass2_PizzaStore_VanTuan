using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ass2_PizzaStore_VanTuan.Models
{
    public class Account
    {
        [Key]
        [Required]
        public Guid AccountID { get; set; }

        [Required(ErrorMessage = "User Name is required.")]
        [Column(TypeName = "varchar(100)")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [Column(TypeName = "varchar(20)")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        [Column(TypeName = "varchar(100)")]
        public string FullName { get; set; }
        [Range(0, 1, ErrorMessage = "Only accept 1 or 0.")]
        public byte? Type { get; set; }
    }
}
