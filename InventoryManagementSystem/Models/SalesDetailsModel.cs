using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InventoryManagementSystem.Models
{
    public class SalesDetailsModel
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        [JsonIgnore]
        public ItemModel Item { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public  int SalesMasterId { get; set; }
        [ForeignKey("SalesMasterId")]
        [JsonIgnore]
        public SalesMasterModel SalesMaster { get; set; }
    }
}
