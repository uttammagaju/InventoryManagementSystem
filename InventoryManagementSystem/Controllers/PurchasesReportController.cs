using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class PurchasesReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
