using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Models.VM;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Services
{
    public class SalesReportServices : ISalesReportServices
    {
        private readonly ApplicationDbContext _context;
        public SalesReportServices(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<SalesMasterVM>> GetAll()
        {
            List<SalesMasterVM> result = new List<SalesMasterVM>();
            var masterData = await _context.SalesMaster.ToListAsync();
            if (masterData.Count > 0)
            {
                result = (from m in masterData
                          let customer = _context.Customers.FirstOrDefault(c => c.Id == m.CustomerId)
                          let details = _context.SalesDetails.Where(x => x.SalesMasterId == m.Id)
                          select new SalesMasterVM
                          {
                              Id = m.Id,
                              SalesDate = m.SalesDate,
                              CustomerId = m.CustomerId,
                              CustomerName = customer.FullName,
                              InvoiceNumber = m.InvoiceNumber,
                              BillAmount = m.BillAmount,
                              Discount = m.Discount,
                              NetAmount = m.NetAmount,
                              Sales = (from d in details
                                       select new SalesDetailsVM
                                       {
                                           Id = d.Id,
                                           ItemId = d.ItemId,
                                           Unit = d.Unit,
                                           Quantity = d.Quantity,
                                           Amount = d.Amount,
                                           Price = d.Price
                                       }
                              ).ToList()
                          }).ToList();
            }
            return result;
        }

        public async Task<SalesMasterVM> GetById(int id)
        {
            var masterData = _context.SalesMaster.Find(id);

            if (masterData == null)
            {
                return new SalesMasterVM();
            }
            else
            {
                var details = _context.SalesDetails.Where(x => x.SalesMasterId == masterData.Id).ToList();
                var data = new SalesMasterVM()
                {
                    Id = masterData.Id,
                    SalesDate = masterData.SalesDate,
                    CustomerId = masterData.CustomerId,
                    InvoiceNumber = masterData.InvoiceNumber,
                    BillAmount = masterData.BillAmount,
                    Discount = masterData.Discount,
                    NetAmount = masterData.NetAmount,
                };
                data.Sales = (from d in details
                              select new SalesDetailsVM()
                              {
                                  Id = d.Id,
                                  ItemId = d.ItemId,
                                  Unit = d.Unit,
                                  Quantity = d.Quantity,
                                  Amount = d.Amount,
                                  Price = d.Price
                              }).ToList();
                return data;
            }

        }

        public async Task<IEnumerable<GetCustomersNameVM>> GetCustomersName()
        {
            var customerData = _context.Customers.Select(customer => new GetCustomersNameVM
            {
                Id = customer.Id,
                CustomerName = customer.FullName
            }).ToList();

            return customerData;

        }

        public async Task<IEnumerable<GetItemsNameVM>> GetItemsName()
        {
            var ItemsData = _context.Items.Select(item => new GetItemsNameVM
            {
                Id = item.Id,
                ItemName = item.Name,
            }).ToList();
            return ItemsData;
        }

        public async Task<int> Create(SalesMasterVM salesReport)
        {
            var masterData = new SalesMasterModel()
            {
                Id = 0,
                SalesDate = salesReport.SalesDate,
                CustomerId = salesReport.CustomerId,
                InvoiceNumber = salesReport.InvoiceNumber,
                BillAmount = salesReport.BillAmount,
                Discount = salesReport.Discount,
                NetAmount = salesReport.NetAmount,
            };
            var masterAdd = _context.SalesMaster.Add(masterData);
            _context.SaveChanges();
            if (masterData != null)
            {
                var details = from d in salesReport.Sales
                              select new SalesDetailsModel
                              {
                                  Id = 0,
                                  ItemId = d.ItemId,
                                  Unit = d.Unit,
                                  Quantity = d.Quantity,
                                  Amount = d.Amount,
                                  Price =d.Price,
                                  SalesMasterId = masterData.Id,
                              };
                _context.SalesDetails.AddRange(details);
                _context.SaveChanges();

            }
            return masterAdd.Entity.Id;
        }

        public async Task<bool> Update(SalesMasterVM salesReport)
        {
            var masterData = _context.SalesMaster.Find(salesReport.Id);
            if (masterData == null)
            {
                return false;
            }
            masterData.SalesDate = salesReport.SalesDate;
            masterData.CustomerId = salesReport.CustomerId;
            masterData.InvoiceNumber = salesReport.InvoiceNumber;
            masterData.Discount = salesReport.Discount;
            masterData.BillAmount = salesReport.BillAmount;
            masterData.NetAmount = salesReport.NetAmount;
            await _context.SaveChangesAsync();

            var existingDetail = _context.SalesDetails.Where(x => x.SalesMasterId == masterData.Id);
            _context.SalesDetails.RemoveRange(existingDetail);

            var details = from d in salesReport.Sales
                          select new SalesDetailsModel
                          {
                              ItemId = d.ItemId,
                              Unit = d.Unit,
                              Quantity = d.Quantity,
                              Amount = d.Amount,
                              Price = d.Price,
                              SalesMasterId = masterData.Id,
                          };
            _context.SalesDetails.AddRange(details);
            _context.SaveChanges();
            return true;

        }

        public async Task<bool> Delete(int id)
        {
           var masterDate = _context.SalesMaster.Find(id);
           if(masterDate == null)
            {
                return false;
            }
            else
            {
                var details = _context.SalesDetails.Where(x => x.SalesMasterId ==  masterDate.Id).ToList();
                if(details.Count > 0)
                {
                    _context.SalesDetails.RemoveRange(details);
                    _context.SaveChanges();
                }
                _context.SalesMaster.Remove(masterDate);
                _context.SaveChanges();
                
            }
            return true;
        }


    }
}
