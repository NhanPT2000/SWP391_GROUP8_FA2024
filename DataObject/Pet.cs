using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class Pet
    {
        [Key]
        [Required]
        public Guid PetId {  get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "This field is required.")]
        public string? PetName { get; set; }
        public Guid? SpeciesId { get; set; }
        public Species? Species { get;set; }
        public DateTime? Birthdate { get; set; }
        [Column(TypeName = "nvarchar(128)")]
        public string? Notes { get; set; }
        public Guid? MemberId { get; set; }
        public Member? Member { get; set; }
        public ICollection<Case>? _Cases { get; set; }
    }
}
