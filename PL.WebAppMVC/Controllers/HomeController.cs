using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PL.WebAppMVC.Models;

namespace PL.WebAppMVC.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            Logger = loggerFactory.CreateLogger(GetType().Namespace);
            Logger.LogInformation("created homeController");
        }

        protected ILogger Logger { get; }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}