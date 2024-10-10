using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class Feedback
    {
        public Guid FeedbackId { get; set; }
        public User? Member { get; set; }
        public Guid? ServiceId { get; set; }
        public Service? Service { get; set; }
        public Guid? ProductId { get; set; }
        public Product? Product { get; set; }
        public Guid? MemberId { get; set; }
        [Column(TypeName ="nvarchar(255)")]
        public string? Message { get; set; }
        public DateTime PostDate { get; set; }
    }
}
