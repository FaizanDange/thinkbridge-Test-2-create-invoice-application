using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace InvoiceApp.Models
{
    public class InvoiceItem
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        
        public int InvoiceId { get; set; }
        [JsonIgnore]
        public Invoice Invoice { get; set; }
    }
}
