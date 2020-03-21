using Demo.ModeCore.Order;
using Microsoft.AspNetCore.Mvc;
using Sprite.EntityFrameWorkCore.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.WebApi.Controllers
{
    /// <summary>
    /// home
    /// </summary>
    public class HomeController : ControllerBase
    {
        private readonly DefaultDbContext _defaultDbContext;
        public HomeController(DefaultDbContext defaultDbContext)
        {
            _defaultDbContext = defaultDbContext;
        }

        public IActionResult Index()
        {
            _defaultDbContext.Set<OrderItem>().ToList();
            return Ok();
        }
    }
}
