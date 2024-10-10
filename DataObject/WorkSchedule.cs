using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class WorkSchedule
    {
        public Guid WorkScheduleId { get; set; }
        public string? Image {  get; set; }
        public ICollection<StaffDetails>? Staffs { get; set; }
    }
}
