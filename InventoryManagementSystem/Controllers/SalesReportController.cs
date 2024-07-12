using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    public class SalesReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
