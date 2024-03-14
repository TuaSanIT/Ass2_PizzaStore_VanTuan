using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Ass2_PizzaStore_VanTuan.Models
{
    public class Product
    {
        [Key]
        [Required]
        public Guid ProductID { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        [Column(TypeName = "nvarchar(100)")]
        public string ProductName { get; set; }

        public Guid SupplierID { get; set; }

        public Guid CategoryID { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Column(TypeName = "nvarchar(20)")]
        public int QuantityPerUnit { get; set; }

        public decimal UnitPrice { get; set; }
        [Column(TypeName = "nvarchar(300)")]
        public string? ProductImage { get; set; }

        public Supplier Suppliers { get; set; }
        public Categories Category { get; set; }

        // Navigation property to represent the relationship with OrderDetail
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
