using Demo.Core.OrderContract.Dtos;
using Demo.Core.OrderContract.Interface;
using Demo.ModeCore.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.OrderContract
{
    public partial class OrderService : IOrderCommand
    {
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="orderInput"></param>
        /// <returns></returns>
        public async Task CreateOrder(OrderInput orderInput)
        {
            var order = new Order
            {
                Address = orderInput.Address,
                Code = orderInput.Code,
                Price = 1
            };

            var product = await _productRepository.GetAsync(1);
            if (product != null)
                product.Quantity -= 1;

            await _productRepository.UnitOfWork.BeginOrUseTransactionAsync();

            await _productRepository.UpdateAsync(product);
            _productRepository.UnitOfWork.Commit();
        }

    }
}
