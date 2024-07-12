namespace InventoryManagementSystem.Models.VM
{
    public class SalesMasterVM
    {
        public int Id { get; set; }
        public DateTime SalesDate { get; set; }
        public int CustomerId { get; set; }
        public string? Customer { get; set; }
        public int InvoiceNumber { get; set; }
        public decimal BillAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmount { get; set; }
        public List<SalesDetailsVM> sales { get; set; }
    }

    public class SalesDetailsVM
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public decimal price { get; set; }
    }
}

