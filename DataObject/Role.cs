using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class Role
    {
        [Key]
        [Required]
        public Guid RoleId {  get; set; }
        [Column(TypeName ="nvarchar(50)")]
        [Required(ErrorMessage = "This field is required.")]
        public string? RoleName { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? Status { get; set; }
        public ICollection<User>? _Members { get; set; }
    }
}
