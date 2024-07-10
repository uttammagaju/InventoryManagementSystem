using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Services
{
    public interface IItemsServices
    {
        Task<List<ItemModel>> GetAll();
        Task<ItemModel> GetById(int id);
        Task<int> Create(ItemModel item);
        Task<bool> Update(ItemModel item);
        Task<bool> Delete(int id);
    }
}
