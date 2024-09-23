using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class Admin
    {
        [Key]
        [Required]
        [ForeignKey("Member")]
        public Guid AdminId { get; set; }
        public Member? Member { get; set; }
        public ICollection<Event> _Events { get; set; }
    }
}
