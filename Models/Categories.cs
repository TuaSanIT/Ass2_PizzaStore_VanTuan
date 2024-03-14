using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ass2_PizzaStore_VanTuan.Models
{
    public class Categories
    {
        [Key]
        [Required]
        public Guid CategoryID { get; set; }

        [Required(ErrorMessage = "CategoryName is required!")]
        [MaxLength(50)]
        public string CategoryName { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [DisplayFormat(NullDisplayText = "No description.")]
        public string? Description { get; set; }

        ICollection<Product> Products { get; set; }
    }
}
