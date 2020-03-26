using Demo.Core.OrderContract.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.OrderContract.Interface
{
    /// <summary>
    /// 订单-查询接口
    /// </summary>
    public interface IOrderQuery
    {
        /// <summary>
        /// 根据Id查询订单
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        Task<OrderOutput> GetOrderById(int orderId);
    }
}
