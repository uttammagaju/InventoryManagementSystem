using InventoryManagementSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesReportAPIController : ControllerBase
    {
        private readonly ISalesReportServices _salesReportServices;
        public SalesReportAPIController(ISalesReportServices salesReportServices)
        {
            _salesReportServices = salesReportServices;   
        }


    }
}
