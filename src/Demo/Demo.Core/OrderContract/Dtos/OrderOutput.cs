using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Core.OrderContract.Dtos
{
    /// <summary>
    /// 订单输出
    /// </summary>
    public class OrderOutput
    {
        public string Code { get; set; }

        public decimal Price { get; set; }

        public string Address { get; set; }

    }
}
