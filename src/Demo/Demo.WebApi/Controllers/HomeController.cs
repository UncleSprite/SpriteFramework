using Demo.ModeCore.Order;
using Microsoft.AspNetCore.Mvc;
using Sprite.EntityFrameWorkCore;
using Sprite.EntityFrameWorkCore.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.WebApi.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly DefaultDbContext _dbContextBase;
        public HomeController(DefaultDbContext dbContextBase)
        {
            _dbContextBase = dbContextBase;
        }

        public IActionResult Index()
        {
            var data = _dbContextBase.Set<OrderItem>().ToList();
            return Ok(data);
        }
    }
}
