using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class Order
    {
        [Required]
        [Key]
        public Guid OrderId { get; set; }
        /*Has 1 member*/
        public Guid MemberId { get; set; }
        public User? Member { get; set; }
        /**/
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public float Freight { get; set; }
        public HashSet<OrderDetails>? OrderDetails { get;set; }
        public ICollection<Voucher>? _Vouchers { get;set; }

        public bool? IsDeleted { get; set; }
        public bool? IsPaid { get; set; }
        public IEnumerable<Invoice>? Invoices { get; set; }
    }
}
