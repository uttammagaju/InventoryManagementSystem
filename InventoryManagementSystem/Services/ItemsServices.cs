using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;


namespace InventoryManagementSystem.Services
{
    public class ItemsServices : IItemsServices
    {
        private readonly ApplicationDbContext _context;
        public ItemsServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ItemModel>> GetAll()
        {
            return await _context.Items
                .Select(c => new ItemModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Unit = c.Unit,
                    Category = c.Category
                }).ToListAsync();
        }

        public async Task<ItemModel> GetById(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return null;
            }

            return new ItemModel
            {
                Id = item.Id,
                Name = item.Name,
                Unit = item.Unit,
                Category = item.Category
            };
        }

        public async Task<int> Create(ItemModel itemModel)
        {
            var item = new ItemModel
            {
                Id = itemModel.Id,
                Name = itemModel.Name,
                Unit = itemModel.Unit,
                Category = itemModel.Category
            };

            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return item.Id;
        }

        public async Task<bool> Delete(int id)
        {
            var item = _context.Items.Find(id);

            if (item == null)
            {
                return false;
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(ItemModel itemModel)
        {
            var item = await _context.Items.FindAsync(itemModel.Id);

            if (item == null)
            {
                return false;
            }

            item.Name = itemModel.Name;
            item.Unit = itemModel.Unit;
            item.Category = itemModel.Category;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
