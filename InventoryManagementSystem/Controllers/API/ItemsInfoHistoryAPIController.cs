using Inventory.Entities;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InventoryManagementSystem.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsInfoHistoryAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ItemsInfoHistoryAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<ItemsHistoryVM>> GetItemsHistory()
        {
            var items = await _context.Items.ToListAsync();
            var itemsHistory = await _context.ItemsHistoryInfo.ToListAsync();

            var result = (from i in items
                          join h in itemsHistory on i.Id equals h.ItemId
                          select new ItemsHistoryVM
                          {
                              Id = h.Id,
                              ItemId = h.ItemId,
                              ItemName = i.Name,
                              Quantity = h.Quantity,
                              StockCheckOut = h.StockCheckOut,
                              TransactionType = h.TransactionType,
                              TransDate = h.TransDate
                          }).ToList();

            return result;
        }

        public class ItemsHistoryVM
        {
            public int Id { get; set; }
            public int ItemId { get; set; }
            public string ItemName { get; set; }
            public int Quantity { get; set; }
            public DateTime TransDate { get; set; }
            public StockCheckOut StockCheckOut { get; set; }
            public TransactionType TransactionType { get; set; }

            [NotMapped]
            public string TransDateFormatted => TransDate.ToString("yyyy-MM-dd");

            [NotMapped]
            public string StockCheckOutText => StockCheckOut.ToString();

            [NotMapped]
            public string TransactionTypeText => TransactionType.ToString();
        }
    }
}
