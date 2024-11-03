using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class Case
    {
        [Key]
        [Required]
        public Guid CaseId { get; set; }
        public Guid? FacilityId { get; set; }
        public Facility? Facility { get; set; }
        public Guid? PetId {  get; set; }
        public Pet? Pet { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsClosed { get; set; }
        [Column(TypeName ="nvarchar(MAX)")]
        public string? Notes {  get; set; }
        public  HashSet<Invoice>? Invoices { get; set; }
        public HashSet<PlannedService>? _PlannedServices { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
