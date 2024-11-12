using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class Facility
    {
        [Key]
        [Required]
        public Guid FacilityId {  get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        [Required(ErrorMessage = "This field is required.")]
        public string? FacilityName {  get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        [Required(ErrorMessage = "This field is required.")]
        public string? Address { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        [Required(ErrorMessage = "This field is required.")]
        [Phone]
        public string? PhoneNumber { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        [Required(ErrorMessage = "This field is required.")]
        public string? ContractPer { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        public string? Image { get; set; }
        public HashSet<Case>? _Cases { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
