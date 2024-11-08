using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class Invoice
    {
        [Key]
        [Required]
        public Guid InvoiceId { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        [Required(ErrorMessage = "This field is required.")]
        public string? InvoiceCode { get; set; }
        public Guid? CaseId { get; set; }
        public Case? Case { get; set; }
        public Guid? ServiceId { get; set; }
        public Service? Service { get; set; }
        public Order? Order { get; set; }
        public Guid? OrderId { get; set; }
        public int TimeGenerated {  get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal? InvoiceAmount { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal? Discount { get; set; }
        public DateTime TimeCharge { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal? AmountCharge { get; set; }
        [Column(TypeName = "nvarchar(255)")]
        public string? Notes { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? Status { get; set; } //"Paid", "Pending", "Failed"

        [Column(TypeName = "nvarchar(255)")]
        public string? PaymentId { get; set; } // Store the PayPal Payment ID

        [Column(TypeName = "nvarchar(255)")]
        public string? PayerId { get; set; } // Store the PayPal Account ID
    }
}
