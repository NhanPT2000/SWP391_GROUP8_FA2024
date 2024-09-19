using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class Member
    {
        [Key]
        [Required]
        public Guid MemberId { get; set; }
        [Column(TypeName ="nvarchar(50)")]
        [Required(ErrorMessage ="This field is required.")]
        public string? MemberName { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "This field is required.")]
        public string? Password {  get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "This field is required.")]
        [EmailAddress]
        public string? Email { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public bool? Gender { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "This field is required.")]
        [Phone]
        public string? PhoneNumber { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? PhoneNumber2 { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "This field is required.")]
        public string? Addess {  get; set; }

        public ICollection<Pet>? _Pets { get; set; }
        public ICollection<Order>? _Orders { get; set; }

        public Role? _Role { get; set; }
        public Guid RoleId { get; set; }

        public Staff? _Staff { get; set; }
        public Admin? _Admin { get; set; }
    }
}
