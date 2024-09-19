using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class Staff
    {
        [Key]
        [Required]
        [ForeignKey("Member")]
        public Guid StaffId {  get; set; }
        [Column(TypeName ="nvarchar(500)")]
        public string? DegreeImage {  get; set; }
        public Guid WorkScheduleId { get; set; }
        public WorkSchedule? WorkSchedule { get; set; }
        public Member? Member { get; set; }
    }
}
