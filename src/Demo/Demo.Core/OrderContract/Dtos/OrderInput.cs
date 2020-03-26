using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Core.OrderContract.Dtos
{
    public class OrderInput
    {
        public string Code { get; set; }

        public decimal Price { get; set; }

        public string Address { get; set; }
    }
}
