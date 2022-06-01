using Cargo4You.Data.Database.Cargo4You.Context;
using Cargo4You.Services;
using Cargo4You.Services.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cargo4You.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly OrderService _orderService;

        public OrderController(
            Courier4YouContext context,
            ILogger<OrderController> logger,
            OrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [HttpPost("GetPrice", Name = "GetPrice")]
        [ProducesResponseType(typeof(CourierOfferPrice), 200)]
        public async Task<IActionResult> GetPrice(PackageDetailData packageDetails)
        {
            var userId = ControllerContext.HttpContext.User?.Claims?.Where(x => x.Type == "userId").FirstOrDefault();

            return Ok(await _orderService.GetPrice(packageDetails));

        }

    }
}
