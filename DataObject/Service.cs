using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class Service
    {
        [Key]
        [Required]
        public Guid ServiceId { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "This field is required.")]
        public string? ServiceName { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "This field is required.")]
        public string? ServiceDescription { get; set; }
        public bool? HasLimit { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public float CostPerUnity { get; set; }
        public ICollection<Invoice>? _Invoices { get; set; }
        public ICollection<PlannedService>? _PlannedServices { get; set; }
        public ICollection<Feedback>? _Feedbacks { get; set; }
    }
}
