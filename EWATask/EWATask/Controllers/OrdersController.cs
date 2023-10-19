using CORE.Entites;
using CORE.Interfaces;
using CORE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EWATask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> _user;

        public OrdersController(IOrderService _orderService, IHttpContextAccessor _contextAccessor, UserManager<ApplicationUser> _user)
        {
            this._orderService = _orderService;  
            this._contextAccessor = _contextAccessor;
            this._user = _user;
        }
        [HttpPost]
        [Authorize(Policy = "UserCanPlaceOrder")]
        [Route("AddOrdder")]
        public async Task<IActionResult> AddOrdder(AddOrder form)
        {
          

            bool checkAdd = await _orderService.AddOrder(form);
            if (checkAdd)
                return Ok(new { StausCode = 200, Message = "Order added Successfully" });
            else
                return BadRequest(new { StausCode = 500, Message = "Failed To added Order" });
        }
    }
}
