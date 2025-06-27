using Billiard.DTO;
using Billiard.Models;
using Microsoft.EntityFrameworkCore;

namespace Billiard.Repositories.Invoce
{
    public class Invocerepository : IInvoceRepository
    {
        private readonly Prn232ProjectContext _projectContext;

        public Invocerepository(Prn232ProjectContext projectContext)
        {
            _projectContext = projectContext;   
        }

        public async Task<int> addInvoce(CreateInvoice invoice)
        {

            Models.Invoice temp = new Invoice()
            {
                TimeStart = DateTime.Now,
                EmployeeId = invoice.EmployeeId,
                TableId = invoice.TableId,
                PaymentStatus = "Đang chơi",
                TotalAmount = 0

            };

            _projectContext.Invoices.Add(temp);
            await _projectContext.SaveChangesAsync();
            return temp.InvoiceId;
        }

        public async Task<IEnumerable<Invoice>> GetInvoices()
        {
            
                return await _projectContext.Invoices.ToListAsync();
            
            
        }

        public async Task<bool> SaveServiceOfTable(ServiceOfTableModel servicesOfTable)
        {
            try
            {
                foreach (var item in servicesOfTable.Services)
                {
                    InvoiceDetail detail = new InvoiceDetail()
                    {
                        InvoiceId = servicesOfTable.InvoiceId,
                        TableId = servicesOfTable.TableId,
                        ServiceId = item.Id,
                        Quantity = item.Quantity,
                    };
                    await _projectContext.InvoiceDetails.AddAsync(detail);
                    await _projectContext.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> updateInvoice(InvoiceUpdateModel invoiceData)
        {
            try
            {
                Invoice invoice = await _projectContext.Invoices.FindAsync(invoiceData.InvoiceId);
                if (invoice == null) {
                
                    return false;
                }
                invoice.TimeEnd = invoiceData.TimeEnd;
                invoice.TotalAmount = invoiceData.TotalAmount;
                invoice.UserId = invoiceData.UserId;
                invoice.PaymentStatus = invoiceData.PaymentStatus;

                _projectContext.Update(invoice);
                await _projectContext.SaveChangesAsync();

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
