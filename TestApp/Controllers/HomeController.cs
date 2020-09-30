using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TestApp.Context;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public UserManager<User> _userManager;
        public ServerDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ServerDbContext dbContext, UserManager<User> userManager)
        {
            _logger = logger;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            if (User.IsInRole("admin")) return RedirectToAction("Index", "Admin");
            return View();
        }

        [HttpGet]
        public IActionResult Admin()
        {
            var model = new AdminViewModel
            {
                Users = _dbContext.User.ToList(),
            };
            return View(model);
        }

    }
}
