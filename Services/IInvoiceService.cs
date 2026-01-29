using InvoiceApp.Models;
using System.Threading.Tasks;

namespace InvoiceApp.Services
{
    public interface IInvoiceService
    {
        Task<Invoice> GetInvoiceAsync(int id);
        Task<Invoice> CreateInvoiceAsync(Invoice invoice);
    }
}
