using DataObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IInvoiceService
    {
        Task<IEnumerable<Invoice>> GetInvoicesAsync();
        Task<Invoice?> GetInvoiceByIdAsync(Guid id);
        Task<bool> UpdateInvoiceAsync(Invoice invoice, Guid id);
        Task<bool> DeleteInvoiceAsync(Guid id);
        Task CreateInvoiceAsync(Invoice invoice);
        Task<Invoice?> GetInvoiceByOrderIdAsync(Guid id);
    }
}
