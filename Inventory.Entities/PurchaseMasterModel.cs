using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Inventory.Entities
{
    public class PurchaseMasterModel
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        [ForeignKey("VendorId")]
        [JsonIgnore]
        public virtual VendorModel Vendor { get; set; }
        public int InvoiceNumber { get; set; }
        public decimal BillAmount {get; set; }
        public decimal Discount  { get; set;}
        public decimal NetAmount { get; set; }
    }

}
