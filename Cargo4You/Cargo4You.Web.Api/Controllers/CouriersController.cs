using Cargo4You.Data.Database.Cargo4You.Context;
using Cargo4You.Data.Database.Cargo4You.Model;
using Cargo4You.Services;
using Cargo4You.Services.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cargo4You.Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CouriersController : ControllerBase
    {
        private readonly ILogger<CouriersController> _logger;
        private readonly CourierService _courierService;

        public CouriersController(
            Courier4YouContext context,
            ILogger<CouriersController> logger,
            CourierService courierService)
        {
            _logger = logger;
            _courierService = courierService;
        }

        [HttpGet("GetCouriers", Name = "GetCouriers")]
        [ProducesResponseType(typeof(List<CourierData>), 200)]
        public async Task<IActionResult> GetCouriers()
        {
            return Ok(await _courierService.GetAllCourier());
        }

        [HttpPost("AddCourier", Name = "AddCourier")]
        [ProducesResponseType(typeof(List<bool>), 200)]
        public async Task<IActionResult> AddCourier(AddCourierData courierData)
        {
            return Ok(await _courierService.AddCourier(courierData));

        }

        [HttpPost("AddPrices", Name = "AddPrices")]
        [ProducesResponseType(typeof(List<bool>), 200)]
        public async Task<IActionResult> AddPrices(List<CourierPriceData> pricesData)
        {
            return Ok(await _courierService.AddCourierPrices(pricesData));
        }
    }
}
