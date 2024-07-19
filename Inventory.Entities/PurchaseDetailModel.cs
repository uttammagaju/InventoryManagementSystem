using InventoryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Inventory.Entities
{
    public class PurchaseDetailModel
    {
        public int Id { get; set; }
        public int ItemId {  get; set; }
        [ForeignKey("ItemId")]
        public virtual ItemModel Item { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public int PurchaseMasterId { get; set; }
        [ForeignKey("PurchaseMasterId")]
        [JsonIgnore]
        public virtual PurchaseMasterModel PurchaseMaster { get; set; }

    }
}
