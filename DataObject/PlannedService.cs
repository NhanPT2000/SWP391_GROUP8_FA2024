using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DataObject
{
    public class PlannedService
    {
        [Key]
        [Required]
        public Guid PlannedServiceId { get; set; }
        public Guid? CaseId { get; set; }
        public Case? Case { get; set; }
        public Guid? ServiceId { get; set; }
        public Service? Service { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int PlannedUnits {  get; set; }
        [Column(TypeName ="decimal(10,2)")]
        public float CostPerUnit {  get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public float Price { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        public string? Notes { get; set; }
        public ICollection<Voucher>? _Vouchers { get; set; }
        public User? _Member { get; set; }
        public Guid? MemberId { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
