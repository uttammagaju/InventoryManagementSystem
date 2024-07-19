using InventoryManagementSystem.Models;
using InventoryManagementSystem.Models.VM;

namespace InventoryManagementSystem.Services
{
    public interface IPurchasesReportServices 
    {
        Task<List<PurchasesMasterVM>> GetAll();
        Task<PurchasesMasterVM> GetById(int id);
        Task<int> Create(PurchasesMasterVM purchasesReport);
        Task<bool> Update(PurchasesMasterVM purchaseReport);
        Task<bool> Delete(int id);
        Task<IEnumerable<GetVendorsNameVM>> GetVendorsName();
        Task<IEnumerable<GetItemsNameVM>> GetItemsName();
    }
}
