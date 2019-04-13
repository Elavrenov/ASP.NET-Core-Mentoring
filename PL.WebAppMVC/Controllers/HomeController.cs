using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PL.WebAppMVC.Filters;
using PL.WebAppMVC.Models;
using SmartBreadcrumbs.Attributes;

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

        [DefaultBreadcrumb("Home")]
        [TypeFilter(typeof(ActionFilter),
            Arguments = new object[] { "Method 'Index' controller 'Home'" })]
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