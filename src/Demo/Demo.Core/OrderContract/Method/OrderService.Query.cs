using Demo.Core.OrderContract.Dtos;
using Demo.Core.OrderContract.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.OrderContract
{
    public partial class OrderService : IOrderQuery
    {
        public async Task<OrderOutput> GetOrderById(int orderId)
        {
            var order = await _orderRepository.GetAsync(orderId);

            if (order == null)
                return new OrderOutput();

            return new OrderOutput
            {
                Address = order.Address,
                Code = order.Code,
                Price = order.Price
            };
        }
    }
}
