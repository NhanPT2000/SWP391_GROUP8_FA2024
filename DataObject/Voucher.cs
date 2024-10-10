using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObject
{
    public class Voucher
    {
        [Key]
        [Required]
        public Guid Voucherid { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "This field is required.")]
        public string? VourcherTitle {  get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "This field is required.")]
        public string? VourcherDescription { get; set;}
        public int Discount { get; set; }

        public PlannedService? PlannedService { get; set; }
        public Guid? PlannedServiceId { get; set; }
        public Order? Order { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? UserId { get; set; }
        public User? User { get; set; }
    }
}
