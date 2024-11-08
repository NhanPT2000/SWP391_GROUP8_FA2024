using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class User
    {
        [Key]
        [Required]
        public Guid UserId { get; set; }
        [Column(TypeName ="nvarchar(255)")]
        [Required(ErrorMessage ="This field is required.")]
        public string? UserName { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [Required(ErrorMessage = "This field is required.")]
        public string? Password {  get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [Required(ErrorMessage = "This field is required.")]
        [EmailAddress]
        public string? Email { get; set; }
        [Column(TypeName = "nvarchar(256)")]
        public string? Gender { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "This field is required.")]
        [Phone]
        public string? PhoneNumber { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? PhoneNumber2 { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [Required(ErrorMessage = "This field is required.")]
        public string? Addess {  get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Profile {  get; set; }
        public DateTime? OnlineTime { get; set; }
        public IEnumerable<Pet>? _Pets { get; set; }
        public IEnumerable<Order>? _Orders { get; set; }
        public bool? ConfirmedEmail { get; set; }
        public Role? _Role { get; set; }
        public Guid? RoleId { get; set; }

        public StaffDetails? _StaffDetails { get; set; }
        public IEnumerable<Event>? _Events { get; set; }
        public IEnumerable<PlannedService>?_PlannedServices { get; set; }
        public ICollection<Feedback>? _Feedbacks { get; set; }
        public ICollection<Voucher>? _Vouchers { get; set; }
    }
}
