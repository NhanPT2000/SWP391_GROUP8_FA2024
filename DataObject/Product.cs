using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class Product
    {
        [Key]
        [Required]
        public Guid ProductId { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Product name is required.")]
        public string? ProductName { get; set; }
        [Column(TypeName = "nvarchar(900)")]
        [Required(ErrorMessage = "Description is required.")]
        public string? ProductDescription { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public Category? Category { get; set; }
        public Guid? CategoryId {  get; set; }
        public string? Origin { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public float Weight { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public float UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        [Column(TypeName = "nvarchar(Max)")]
        public string? Image {  get; set; }

        public HashSet<OrderDetails>? _OrderDetails { get; set; }
        public ICollection<Feedback>? _Feedbacks { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
