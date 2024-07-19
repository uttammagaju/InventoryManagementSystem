using Inventory.Entities;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Services
{
    public interface IVendorServices
    {
        Task<List<VendorModel>> GetAll();
        Task<VendorModel> GetById(int id);
        Task<int> Create (VendorModel vendor);
        Task<bool> Update (VendorModel vendor);
        Task<bool> Delete (int id);
        Task<List<VendorModel>> SearchVendor(string searchTerm);
    }
}
