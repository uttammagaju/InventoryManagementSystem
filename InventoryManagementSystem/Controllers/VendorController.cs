using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class VendorController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
