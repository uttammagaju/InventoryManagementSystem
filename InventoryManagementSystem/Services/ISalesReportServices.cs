using InventoryManagementSystem.Models;
using InventoryManagementSystem.Models.VM;

namespace InventoryManagementSystem.Services
{
    public interface ISalesReportServices 
    {
        Task<List<SalesMasterVM>> GetAll();
        Task<SalesMasterVM> GetById(int id);
        Task<int> Create(SalesMasterVM salesReport);
        Task<bool> Update(SalesMasterVM salesReport);
        Task<bool> Delete(int id);
        Task<IEnumerable<GetCustomersNameVM>> GetCustomersName();
        Task<IEnumerable<GetItemsNameVM>> GetItemsName();
    }
}
