using Demo.Core.OrderContract.Dtos;
using Demo.Core.OrderContract.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.WebApi.Controllers
{
    /// <summary>
    /// 订单
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        readonly IOrderCommand _command;
        readonly IOrderQuery _query;

        public OrderController(IOrderCommand command, IOrderQuery query)
        {
            _command = command;
            _query = query;
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="orderInput"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderInput orderInput)
        {
            await _command.CreateOrder(orderInput);
            return Ok();
        }
    }
}
