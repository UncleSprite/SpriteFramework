using Demo.ModeCore.Order;
using Microsoft.Extensions.DependencyInjection;
using Sprite.Common.Dependency;
using Sprite.Common.Entity;
using System;

namespace Demo.Core.OrderContract
{
    /// <summary>
    /// 订单域_订单服务--服务发现
    /// </summary>
    public partial class OrderService : IScopeDependency
    {
        //readonly IServiceProvider _serviceProvider;
        private readonly IRepository<Order, int> _orderRepository;
        private readonly IRepository<OrderItem, int> _orderItemRepository;
        private readonly IRepository<Product, int> _productRepository;

        //public OrderService(IServiceProvider serviceProvider)
        //{
        //    //_serviceProvider = serviceProvider;

        //    _orderItemRepository = _serviceProvider.GetService<IRepository<OrderItem, int>>();
        //    _orderRepository = _serviceProvider.GetService<IRepository<Order, int>>();

        //}

        public OrderService(IRepository<Order, int> orderRepository, IRepository<OrderItem, int> orderItemRepository, IRepository<Product, int> productRepository)
        {
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }
    }
}
