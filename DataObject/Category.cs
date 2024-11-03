using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class Category
    {
        [Key]
        [Required]
        public Guid CategoryId {  get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "This field is required.")]
        public string? CategoryName { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? Status { get; set; }
        public HashSet<Product>? _Products { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
