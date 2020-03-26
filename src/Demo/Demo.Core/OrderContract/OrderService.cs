using Sprite.Common.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Core.OrderContract
{
    /// <summary>
    /// 订单域_订单服务--服务发现
    /// </summary>
    public partial class OrderService : IScopeDependency
    {
        private readonly IServiceProvider _serviceProvider;
        public OrderService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
    }
}
