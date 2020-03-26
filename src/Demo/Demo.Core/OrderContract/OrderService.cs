using Demo.ModeCore.Order;
using Sprite.Common.Dependency;
using Sprite.Common.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Core.OrderContract
{
    /// <summary>
    /// 订单域_订单服务--服务发现
    /// </summary>
    public partial class OrderService : IScopeDependency
    {
        readonly IServiceProvider _serviceProvider;
        readonly IRepository<Order, int> _orderRepository;
        readonly IRepository<OrderItem, int> _orderItemRepository;
        public OrderService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _orderItemRepository = _serviceProvider.GetService<IRepository<OrderItem, int>>();
            _orderRepository = _serviceProvider.GetService<IRepository<Order, int>>();
        }
    }
}
