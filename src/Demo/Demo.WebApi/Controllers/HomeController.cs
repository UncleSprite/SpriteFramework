using Demo.ModeCore.Order;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
=======
using Sprite.EntityFrameWorkCore;
>>>>>>> 555ac816375452fa1c703c35e1dc31430af86d73
using Sprite.EntityFrameWorkCore.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.WebApi.Controllers
{
<<<<<<< HEAD
    /// <summary>
    /// home
    /// </summary>
    public class HomeController : ControllerBase
    {
        private readonly DefaultDbContext _defaultDbContext;
        public HomeController(DefaultDbContext defaultDbContext)
        {
            _defaultDbContext = defaultDbContext;
=======
    public class HomeController : ControllerBase
    {
        private readonly DefaultDbContext _dbContextBase;
        public HomeController(DefaultDbContext dbContextBase)
        {
            _dbContextBase = dbContextBase;
>>>>>>> 555ac816375452fa1c703c35e1dc31430af86d73
        }

        public IActionResult Index()
        {
<<<<<<< HEAD
            _defaultDbContext.Set<OrderItem>().ToList();
            return Ok();
=======
            var data = _dbContextBase.Set<OrderItem>().ToList();
            return Ok(data);
>>>>>>> 555ac816375452fa1c703c35e1dc31430af86d73
        }
    }
}
