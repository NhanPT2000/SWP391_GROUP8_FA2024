using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class Species
    {
        [Key]
        [Required]
        public Guid SpeciesId { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "This field is required.")]
        public string? SpeciesName { get; set; }
        public HashSet<Pet>? Pets { get; set; }

        public bool? IsDelete { get; set; }
    }
}
