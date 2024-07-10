

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InventoryManagementSystem.Models
{
    public class SalesMasterModel
    {
        public int Id { get; set; }
        public DateTime SalesDate { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        [JsonIgnore]
        public virtual CustomerModel Customer { get; set; }
        public int InvoiceNumber { get; set; }
        public decimal BillAmount { get; set; }
        public decimal Discount { get; set;}
        public decimal NetAmount { get; set; }

    }
}
