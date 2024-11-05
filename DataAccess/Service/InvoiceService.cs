using DataAccess.Database;
using DataAccess.Repository;
using DataObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Service
{
    public class InvoiceService : IInvoiceService
    {
        private readonly PetShopContext _serviceContext;

        public InvoiceService(PetShopContext serviceContext)
        {
            _serviceContext = serviceContext;
        }

        public async Task CreateInvoiceAsync(Invoice invoice)
        {
            using var transaction = await _serviceContext.Database.BeginTransactionAsync();
            try
            {
                var newInvoice = new Invoice
                {
                    InvoiceId = invoice.InvoiceId,
                    InvoiceCode = invoice.InvoiceCode,
                    CaseId = invoice.CaseId,
                    ServiceId = invoice.ServiceId,
                    TimeGenerated = invoice.TimeGenerated,
                    InvoiceAmount = invoice.InvoiceAmount,
                    Discount = invoice.Discount,
                    TimeCharge = invoice.TimeCharge,
                    AmountCharge = invoice.AmountCharge,
                    Notes = invoice.Notes
                };

                await _serviceContext.Invoices.AddAsync(newInvoice);
                await _serviceContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesAsync()
        {
            return await _serviceContext.Invoices
                .Include(i => i.Case)
                .Include(i => i.Service)
                .ToListAsync();
        }

        public async Task<Invoice?> GetInvoiceByIdAsync(Guid id)
        {
            return await _serviceContext.Invoices
                .Include(i => i.Case)
                .Include(i => i.Service)
                .FirstOrDefaultAsync(i => i.InvoiceId == id);
        }

        public async Task<bool> UpdateInvoiceAsync(Invoice invoice, Guid id)
        {
            var invoiceToUpdate = await _serviceContext.Invoices.FirstOrDefaultAsync(i => i.InvoiceId == id);

            if (invoiceToUpdate == null)
            {
                return false;
            }

            invoiceToUpdate.InvoiceCode = invoice.InvoiceCode;
            invoiceToUpdate.CaseId = invoice.CaseId;
            invoiceToUpdate.ServiceId = invoice.ServiceId;
            invoiceToUpdate.TimeGenerated = invoice.TimeGenerated;
            invoiceToUpdate.InvoiceAmount = invoice.InvoiceAmount;
            invoiceToUpdate.Discount = invoice.Discount;
            invoiceToUpdate.TimeCharge = invoice.TimeCharge;
            invoiceToUpdate.AmountCharge = invoice.AmountCharge;
            invoiceToUpdate.Notes = invoice.Notes;

            _serviceContext.Invoices.Update(invoiceToUpdate);
            return await _serviceContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteInvoiceAsync(Guid id)
        {
            var invoiceToDelete = await _serviceContext.Invoices.FindAsync(id);
            if (invoiceToDelete == null) return false;

            _serviceContext.Invoices.Remove(invoiceToDelete);
            return await _serviceContext.SaveChangesAsync() > 0;
        }
    }
}
