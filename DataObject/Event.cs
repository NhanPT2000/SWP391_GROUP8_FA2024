using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class Event
    {
        [Key]
        [Required]
        public Guid EventId { get; set; }
        [Column(TypeName ="nvarchar(128)")]
        [Required(ErrorMessage ="This field is required.")]
        public string? EventTitle { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        [Required(ErrorMessage = "This field is required.")]
        public string? Description { get; set; }
        public Admin? Admin { get; set; }
        public Guid? AdminId { get; set; }
        public DateTime PostDate { get; set; }
    }
}
