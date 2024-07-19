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
    public class ItemCurrentInfoHistory
    {
        public int Id { get; set; }

        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        [JsonIgnore]
        public ItemModel Item { get; set; }

        public int Quantity { get; set; }
        public DateTime TransDate { get; set; }

        public StockCheckOut StockCheckOut { get; set; }

        public TransactionType TransactionType { get; set; }
    }

    public enum StockCheckOut
    {
        In,
        Out
    }

    public enum TransactionType
    {
        Purchase,
        Sales
    }
}

