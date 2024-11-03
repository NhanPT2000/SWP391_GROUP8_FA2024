using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class OrderDetails
    {
        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public float? UnitPrice { get; set; }
        public int? Quantity { get; set; }
        public float? Discount { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
