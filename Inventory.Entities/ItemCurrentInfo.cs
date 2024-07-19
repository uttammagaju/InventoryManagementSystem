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
    public class ItemCurrentInfo
    {
        public int Id { get; set; }

        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        [JsonIgnore]
        public virtual ItemModel Item { get; set; }

        public int quantity { get; set; }
    }
}
