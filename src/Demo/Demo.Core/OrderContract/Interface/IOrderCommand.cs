using Demo.Core.OrderContract.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.OrderContract.Interface
{
    public interface IOrderCommand
    {
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="orderInput"></param>
        /// <returns></returns>
        Task CreateOrder(OrderInput orderInput);
    }
}
